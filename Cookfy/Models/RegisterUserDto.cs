namespace Cookfy.Models;

public class RegisterUserDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string? Photo { get; set; }
    public string? Description { get; set; }
}