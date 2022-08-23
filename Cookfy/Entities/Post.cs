using Microsoft.EntityFrameworkCore;

namespace Cookfy.Entities;

public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public string Photo { get; set; }
    public string Ingredient { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}