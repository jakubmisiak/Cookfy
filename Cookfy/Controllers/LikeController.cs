using Cookfy.Entities;
using Cookfy.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cookfy.Controllers;

[Route("api/post/{postId}/Like")]
public class LikeController : ControllerBase
{
    private readonly ILikeService _service;

    public LikeController(ILikeService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromRoute] int postId)
    {
        await _service.Post(postId);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<Like>>> Get([FromRoute] int postId)
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