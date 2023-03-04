namespace Cookfy.Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? UserId { get; set; }
    public virtual User User { get; set; }
    public string Photo { get; set; }
    public DateTime Date { get; set; }
    public string Value { get; set; }
    public virtual List<Like> Likes { get; set; }
}