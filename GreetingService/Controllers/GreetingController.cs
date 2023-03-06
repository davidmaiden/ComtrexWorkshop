using Dapr.Client;
using Google.Protobuf.WellKnownTypes;
using GreetingService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreetingService.Controllers;

[Route("[controller]")]
[ApiController]
public class GreetingController : ControllerBase
{
    private readonly DaprClient _daprClient;

    public GreetingController(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _daprClient.GetStateAsync<string>("greetingstate", id.ToString());

        if (string.IsNullOrEmpty(result))
            return await Task.FromResult(NotFound());

        return await Task.FromResult(Ok(result));
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Post(int id, [FromBody] GreetingValue greetingValue)
    {
        if (greetingValue is null)
            return await Task.FromResult(BadRequest());

        if (string.IsNullOrEmpty(greetingValue.Greeting))
            return await Task.FromResult(BadRequest());

        await _daprClient.SaveStateAsync("greetingstate", id.ToString(), greetingValue.Greeting);
        return await Task.FromResult(Ok());
    }
}
