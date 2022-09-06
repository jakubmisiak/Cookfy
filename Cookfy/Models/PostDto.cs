using Cookfy.Entities;

namespace Cookfy.Models;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
    public virtual UserDto User { get; set; }
    public string Photo { get; set; }
    public string Ingredient { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}