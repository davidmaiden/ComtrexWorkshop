using Web.BFF.Interfaces;

namespace Web.BFF.Services
{
    public class UserService : IUserService
    {
        public async Task<object> GetUserAsync(string userId)
        {
            return await Task.FromResult(new { Id = Guid.Parse("f5f998af-1987-4657-b449-4ac3ab0dc0a1"), Name = "davidm", UserType = "TenantUser", AccessibleTenants = new KeyValuePair<Guid, string>[] { new KeyValuePair<Guid, string>(Guid.Parse("1be4ef1c-921c-4075-a10f-9b74cb292e81"), "Greene King") } });
        }
    }
}
