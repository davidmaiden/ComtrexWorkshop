using Microsoft.AspNetCore.Mvc;

namespace GreetingService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Greetings from {id}";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {

        }
    }
}
