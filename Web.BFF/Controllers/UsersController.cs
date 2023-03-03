using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Web.BFF.Models;

namespace Web.BFF.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        [HttpGet("{userId}/profile")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UserProfile))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserProfile> GetUserProfile(Guid userId)
        {

            return Ok(new UserProfile(Guid.NewGuid(), "Test User", new[] {new TenantValue(Guid.Parse("1be4ef1c-921c-4075-a10f-9b74cb292e81"), "Greene King") }));
        }
    }
}
