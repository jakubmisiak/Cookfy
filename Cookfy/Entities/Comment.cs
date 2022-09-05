using System.ComponentModel.DataAnnotations;

namespace Cookfy.Entities;

public class Comment
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public virtual User User { get; set; }
    public int PostId { get; set; }
    public DateTime Date { get; set; }
    public string Value { get; set; }
}