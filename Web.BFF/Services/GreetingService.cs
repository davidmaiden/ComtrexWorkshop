﻿using System.Net.Http;
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
        var response = await _httpClient.GetAsync($"/v1/greeting/{id}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task SaveGreetingAsync(int id, string greeting)
    {
        var body = new StringContent(greeting);
        body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.PostAsync($"/v1/greeting/{id}", body);

        response.EnsureSuccessStatusCode();
    }
}
