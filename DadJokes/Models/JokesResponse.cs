namespace DadJokes.Models;

public class JokesResponseModel
{
    public int CurrentPage { get; set; }

    public int Limit { get; set; }

    public int NextPage { get; set; }

    public int PreviousPage { get; set; }

    public List<JokeModel>? Results { get; set; }
    public string? SearchTerm { get; set; }
    public int TotalJokes { get; set; }

    public int TotalPages { get; set; }
}