using Microsoft.AspNetCore.Mvc;
using Web.BFF.Interfaces;

namespace Web.BFF.Controllers;

[Route("[Controller]")]
[ApiController]
public class GreetingController : ControllerBase
{
    private readonly IGreetingService _greetingService;

    public GreetingController(IGreetingService greetingService)
    {
        _greetingService = greetingService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGreeting(string id)
    {
        var result = await _greetingService.GetGreetingByIdAsync(id);
        return await Task.FromResult(Ok(result));
    }

    [HttpPost()]
    public async Task<IActionResult> AddGreeting([FromBody] string greeting)
    {


        return await Task.FromResult(Ok());
    }
}
