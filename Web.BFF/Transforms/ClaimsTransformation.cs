using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using Web.BFF.Interfaces;

namespace Web.BFF.Transforms
{
    public class ClaimsTransformation : IClaimsTransformation
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        public ClaimsTransformation(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // return if issued from Client Credentials - not valid
            if (principal?.Claims.FirstOrDefault(c => c.Type == "azp") is null)
                return await Task.FromResult(principal!);

            var subject = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (subject is null)
                return await Task.FromResult(principal!);

            // Get the Tenant Context from the header
            _httpContextAccessor!.HttpContext!.Request.Headers.TryGetValue("x-tenant-id", out var tenantHeader);

            if (StringValues.IsNullOrEmpty(tenantHeader))
                return await Task.FromResult(principal!);

            // Call to the Registered Application Service - to get what this application is.... Events, ZCF, Mobile etc.
            dynamic user = await _userService.GetUserAsync(subject);

            if (user is null)
                return await Task.FromResult(principal!);

            var accessibleTenants = new List<KeyValuePair<Guid, string>>(user.AccessibleTenants);

            if (!accessibleTenants.Any(x => x.Key == Guid.Parse(tenantHeader)))
                return await Task.FromResult(principal!);

            ClaimsPrincipal clone = principal!.Clone();
            var newIdentity = clone.Identity as ClaimsIdentity;

            // add the tenant id as a claim -  for instance
            if (!principal.HasClaim(claim => claim.Type == "TenantId" && claim.Value == tenantHeader))
                newIdentity?.AddClaim(new Claim("TenantId", tenantHeader));

            if (!principal.HasClaim(claim => claim.Type == "UserType" && claim.Value == user.UserType))
                newIdentity?.AddClaim(new Claim("UserType", user.UserType));

            if (user.UserType == "TenantUser")
            {
                if (!principal.HasClaim(claim => claim.Type == ClaimTypes.Role && claim.Value == "tenant"))
                    newIdentity?.AddClaim(new Claim(ClaimTypes.Role, "tenant"));
            }

            if (user.UserType == "ZonalAdmin")
            {
                if (!principal.HasClaim(claim => claim.Type == ClaimTypes.Role && claim.Value == "zonal_admin"))
                    newIdentity?.AddClaim(new Claim(ClaimTypes.Role, "zonal_admin"));
            }

            if (user.UserType == "CustomerPartner")
            {
                if (!principal.HasClaim(claim => claim.Type == ClaimTypes.Role && claim.Value == "customer_partner"))
                    newIdentity?.AddClaim(new Claim(ClaimTypes.Role, "customer_partner"));
            }

            return await Task.FromResult(clone);
        }
    }
}
