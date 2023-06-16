namespace MultipleDbContexts.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Character> Characters { get; set; } = new List<Character>();
}