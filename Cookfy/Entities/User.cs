using System.ComponentModel.DataAnnotations;

namespace Cookfy.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? Photo { get; set; }
    public string? Description { get; set; }
}