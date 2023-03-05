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
        try
        {
            var result = await _greetingService.GetGreetingByIdAsync(id);
            return await Task.FromResult(Ok(result));
        }
        catch (Exception ex) 
        {
            return await Task.FromResult(NotFound());
        }
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> AddGreeting(int id, [FromBody] string greeting)
    {
        if (string.IsNullOrEmpty(greeting))
            return await Task.FromResult(BadRequest());

        try
        {
            await _greetingService.SaveGreetingAsync(id, greeting);
            return await Task.FromResult(Ok());
        }
        catch (Exception ex)
        {
            return await Task.FromResult(BadRequest());
        }
    }
}
