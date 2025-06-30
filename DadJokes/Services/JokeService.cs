using DadJokes.Models;
using System;
using System.Net.Http;

namespace DadJokes.Services;

public static class JokeService
{
    static List<Joke> Jokes { get; }
    static int nextId = 3;
    static JokeService()
    {
        Jokes = new List<Joke>
        {
            new Joke { Id = 1, Text = "asdf" },
        };
    }

    public static List<Joke> GetAll() => Jokes;


    public static Joke? Get(int id) => Jokes.FirstOrDefault(j => j.Id == id);

    public static void Add(Joke joke)
    {
        joke.Id = nextId++;
        Jokes.Add(joke);
    }

    public static void Delete(int id)
    {
        var joke = Get(id);
        if (joke is null)
            return;

        Jokes.Remove(joke);
    }

    public static void Update(Joke joke)
    {
        var index = Jokes.FindIndex(j => j.Id == joke.Id);
        if (index == -1)
            return;

        Jokes[index] = joke;
    }
}