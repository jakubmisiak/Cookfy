namespace Cookfy.Models;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string? Photo { get; set; }
    public string? Description { get; set; }
}