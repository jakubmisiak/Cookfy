using System.Runtime.CompilerServices;
using Cookfy.Models;
using Cookfy.Services;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult> RegisterUser([FromBody]RegisterUserDto dto)
    {
        await _userService.RegisterUser(dto);
        return Ok();
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var userDtos = await _userService.GetAll();

        return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>>GetUser([FromRoute]int id)
    {
        var user = await _userService.GetUser(id);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute]int id)
    {
        await _userService.DeleteUser(id);
        return Ok();
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<ActionResult> UpdateUser([FromBody]UpdateUserDto dto)
    {
        await _userService.UpdateUser(dto);
        return Ok();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _userService.GenerateJwt(dto);
        return Ok(token);
    }
    
    [HttpGet("search/{name}")]
    public async Task<ActionResult<List<UserDto>>> GetByName([FromRoute]string name, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var dtos = await _userService.FindByName(name, pageNumber, pageSize);
        return Ok(dtos);
    }

    [HttpGet("followed")]
    public async Task<ActionResult<List<UserDto>>> GetFollowedUser([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var dtos = await _userService.GetFollowedUsers(pageNumber, pageSize);
        return Ok(dtos);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentLoggedUser()
    {
        var user = await _userService.GetCurrentLoggedUser();
        return Ok(user);
    }

    [HttpPost("isLogged")]
    [Authorize]
    public ActionResult IsUserLogged()
    {
        return Ok(true);
    }
}