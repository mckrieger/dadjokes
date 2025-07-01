using DadJokes.Models;
using DadJokes.Services;
using Microsoft.AspNetCore.Mvc;

namespace DadJokes.Controllers;

[ApiController]
[Route("[controller]")]
public class JokeController : ControllerBase
{
    private readonly IJokeService _jokeService;

    public JokeController(IJokeService jokeService)
    {
        _jokeService = jokeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRandom()
    {
        var joke = await _jokeService.GetRandomAsync();

        if (joke == null)
            return NotFound();

        return Ok(joke);

    }

    [HttpGet("{term}")]
    public async Task<IActionResult> GetByTerm(string term, int limit = 30, int page = 1)
    {
        var jokeList = await _jokeService.GetByTermAsync(term, limit, page);

        //sort list into object

        return Ok(jokeList);
    }
}