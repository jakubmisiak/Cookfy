namespace Cookfy.Entities;

public class Comment
{
    public int Id { get; set; }
    public User User { get; set; }
    public Post Post { get; set; }
    public DateTime Date { get; set; }
    public string Value { get; set; }
}