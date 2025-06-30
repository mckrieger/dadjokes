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
    public JokeService(
        IHttpClientFactory factory
    )
    {
        _factory = factory;
    }

    public async Task<JokeModel?> GetRandomAsync()
    {
        var client = _factory.CreateClient("ICanHazDadJoke");

        JokeModel? response = await client.GetFromJsonAsync<JokeModel>("");
        return response;
    }
}