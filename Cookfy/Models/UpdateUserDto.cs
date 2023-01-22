namespace Cookfy.Models;

public class UpdateUserDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Description { get; set; }
    public string Photo { get; set; }
}