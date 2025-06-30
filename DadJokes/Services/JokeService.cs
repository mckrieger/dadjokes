using DadJokes.Models;
using System;
using System.Net.Http;

namespace DadJokes.Services;

public interface IJokeService
{
    Task<JokeModel?> GetRandomAsync();
}
public class JokeService : IJokeService
{
    private readonly IHttpClientFactory _factory;
    private readonly static string _userAgent = "DadJokes Project (https://github.com/mckrieger/dadjokes)";
    public JokeService(
        IHttpClientFactory factory
    )
    {
        _factory = factory;
    }

    public async Task<JokeModel?> GetRandomAsync()
    {
        using var client = _factory.CreateClient();

        client.DefaultRequestHeaders.Add("User-Agent", _userAgent);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.BaseAddress = new Uri("https://icanhazdadjoke.com/");

        JokeModel? response = await client.GetFromJsonAsync<JokeModel>("");
        return response;
    }
}