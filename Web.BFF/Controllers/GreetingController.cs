using Microsoft.AspNetCore.Mvc;

namespace Web.BFF.Controllers;

[Route("[Controller]")]
[ApiController]
public class GreetingController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGreeting(int id)
    {


        return await Task.FromResult(Ok(""));
    }

    [HttpPost()]
    public async Task<IActionResult> AddGreeting([FromBody] string greeting)
    {


        return await Task.FromResult(Ok());
    }
}
