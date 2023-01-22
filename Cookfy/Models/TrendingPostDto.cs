using Cookfy.Entities;

namespace Cookfy.Models;

public class TrendingPostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
    public virtual UserDto User { get; set; }
    public int LikeCount { get; set; }
    public int LikeId { get; set; }
    public virtual ICollection<Like> Likes { get; set; }
    public string Photo { get; set; }
    public DateTime Date { get; set; }
    public string Value { get; set; }
}