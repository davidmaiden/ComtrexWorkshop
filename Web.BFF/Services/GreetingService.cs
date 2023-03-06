using Web.BFF.Interfaces;
using Web.BFF.Models;

namespace Web.BFF.Services;

public class GreetingService : IGreetingService
{
    private HttpClient _httpClient;

    public GreetingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> GetGreetingByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"/v1/greeting/{id}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task SaveGreetingAsync(int id, string greeting)
    {
        var serializedGreeting = System.Text.Json.JsonSerializer.Serialize(new GreetingValue(greeting));

        var msg = new HttpRequestMessage(HttpMethod.Post, $"/v1/greeting/{id}");
        msg.Content = new StringContent(serializedGreeting, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(msg);

        response.EnsureSuccessStatusCode();
    }
}
