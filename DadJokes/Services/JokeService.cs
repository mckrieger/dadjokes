using DadJokes.Models;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace DadJokes.Services;

public interface IJokeService
{
    Task<JokeModel?> GetRandomAsync();

    Task<JokesLengthGroupModel> GetByTermAsync(string term, int limit, int page);
}
public class JokeService : IJokeService
{
    private readonly IHttpClientFactory _factory;
    private readonly static string _clientName = "ICanHazDadJoke";
    private readonly static char[] _delimiters = new char[] { ' ', '\r', '\n' };
    public JokeService(
        IHttpClientFactory factory
    )
    {
        _factory = factory;
    }

    public async Task<JokeModel?> GetRandomAsync()
    {
        var client = _factory.CreateClient(_clientName);

        JokeModel? response = await client.GetFromJsonAsync<JokeModel>("");
        return response;
    }

    public async Task<JokesLengthGroupModel> GetByTermAsync(string term, int limit, int page)
    {
        var client = _factory.CreateClient(_clientName);
        JokesResponseModel? response = await client.GetFromJsonAsync<JokesResponseModel>($"search?term={term}&limit={limit}&page={page}");
        if (response?.Results == null)
            return null;

        var shortList = new List<JokeModel>();
        var mediumList = new List<JokeModel>();
        var longList = new List<JokeModel>();

        foreach (JokeModel entry in response.Results)
        {
            if (entry == null || string.IsNullOrEmpty(entry.Joke))
                continue;

            var text = entry.Joke;

            var wordcount = text.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            // highlight (Uppercase) search term in joke
            var highlightedText = Regex.Replace(text, term, m => m.Value.ToUpper(), RegexOptions.IgnoreCase);
            Console.WriteLine(highlightedText);

            entry.Joke = highlightedText;

            // add to appropriate list
            if (wordcount < 10)
            {
                shortList.Add(entry);
            }
            else if (wordcount < 20)
            {
                mediumList.Add(entry);
            }
            else
            {
                longList.Add(entry);
            }
        }

        return new JokesLengthGroupModel(shortList, mediumList, longList);
    }
}