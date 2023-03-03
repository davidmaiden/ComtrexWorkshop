using Web.BFF.Interfaces;

namespace Web.BFF.Services;

public class GreetingService : IGreetingService
{
    private HttpClient _httpClient;

    public GreetingService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("GreetingService");
    }

    public async Task<string> GetGreetingByIdAsync(string id)
    {
        var result = await _httpClient.GetAsync($"/greeting/{id}");
        return await result.Content.ReadAsStringAsync();
    }

    public Task SaveGreeting(string greeting)
    {
        throw new NotImplementedException();
    }
}
