using Cookfy.Entities;
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
    
    [HttpPost("{id}")]
    public async Task<ActionResult> Post([FromRoute] int id)
    {
        await _service.Post(id);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<Follow>>> Get([FromRoute] int postId)
    {
        var likes = await _service.Get(postId);
        return Ok(likes);
    }

    [HttpDelete("{likeId}")]
    public async Task<ActionResult> Delete([FromRoute] int postId, [FromRoute] int likeId)
    {
        await _service.Delete(postId, likeId);
        return NoContent();
    }
}