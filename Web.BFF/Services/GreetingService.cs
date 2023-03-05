using Web.BFF.Interfaces;

namespace Web.BFF.Services;

public class GreetingService : IGreetingService
{
    private HttpClient _httpClient;

    public GreetingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetGreetingByIdAsync(string id)
    {
        var result = await _httpClient.GetAsync($"/greeting/{id}");
        return await result.Content.ReadAsStringAsync();
    }

    public async Task SaveGreetingAsync(int id, string greeting)
    {
        var body = new StringContent(greeting);

        var response = await _httpClient.PostAsync($"/greeting/{id}", body);

        response.EnsureSuccessStatusCode();
    }
}
