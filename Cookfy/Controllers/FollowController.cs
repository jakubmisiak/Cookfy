using Cookfy.Entities;
using Cookfy.Models;
using Cookfy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cookfy.Controllers;

[Route("api/follow")]
[Authorize]
public class FollowController : ControllerBase
{
    private readonly IFollowService _service;

    public FollowController(IFollowService service)
    {
        _service = service;
    }
    
    [HttpPost("{userId}")]
    public async Task<ActionResult> Post([FromRoute] int userId)
    {
        await _service.Post(userId);
        return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<FollowDto>> Get([FromRoute] int userId)
    {
        var likes = await _service.Get(userId);
        return Ok(likes);
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult> Delete([FromRoute] int userId)
    {
        await _service.Delete(userId);
        return NoContent();
    }
}