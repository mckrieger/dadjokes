namespace DadJokes.Models;

public class JokesLengthGroupModel
{
    public List<JokeModel>? Short { get; set; }
    public List<JokeModel>? Medium { get; set; }
    public List<JokeModel>? Long { get; set; }
    public JokesLengthGroupModel(List<JokeModel> shortList, List<JokeModel> mediumList, List<JokeModel> longList)
    {
        Short = shortList;
        Medium = mediumList;
        Long = longList;
    }
}