using DadJokes.Models;
using DadJokes.Services;
using Microsoft.AspNetCore.Mvc;

namespace DadJokes.Controllers;

[ApiController]
[Route("[controller]")]
public class JokeController : ControllerBase
{
    public JokeController()
    {
    }

    [HttpGet]
    public ActionResult<List<Joke>> GetAll() =>
        JokeService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Joke> Get(int id)
    {
        var joke = JokeService.Get(id);

        if (joke == null)
            return NotFound();

        return joke;
    }

    [HttpPost]
    public IActionResult Create(Joke joke)
    {
        JokeService.Add(joke);
        return CreatedAtAction(nameof(Get), new { id = joke.Id }, joke);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Joke joke)
    {
        if(id != joke.Id)
            return BadRequest();

        var existingJoke = JokeService.Get(id);
        if(existingJoke is null)
            return NotFound();

        JokeService.Update(joke);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var joke = JokeService.Get(id);

        if(joke is null)
            return NotFound();

        JokeService.Delete(id);

        return NoContent();
    }
}