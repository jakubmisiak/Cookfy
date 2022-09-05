using Cookfy.Models;
using Cookfy.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cookfy.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
    {
        _userService.RegisterUser(dto);
        return Ok();
    }
    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> GetAll()
    {
        var userDtos = _userService.GetAll();

        return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public ActionResult<UserDto>GetUser([FromRoute]int id)
    {
        var user = _userService.GetUser(id);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser([FromRoute]int id)
    {
        _userService.DeleteUser(id);
        return Ok();
    }

    [HttpPut("update/{id}")]
    public ActionResult UpdateUser([FromRoute]int id, [FromBody]RegisterUserDto dto)
    {
        _userService.UpdateUser(id, dto);
        return Ok();
    }
}