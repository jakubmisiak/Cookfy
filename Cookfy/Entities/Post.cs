namespace Cookfy.Entities;

public class Post
{
    public int Id { get; set; }
    public User User { get; set; }
    public string Photo { get; set; }
    public List<string> Ingredient { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}