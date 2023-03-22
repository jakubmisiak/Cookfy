using Cookfy.Entities;
using Cookfy.Models;
using Cookfy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cookfy.Controllers;

[Route("api/post/{postId}/Like")]
[Authorize]
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
    public async Task<ActionResult<List<LikeDto>>> Get([FromRoute] int postId)
    {
        var likes = await _service.Get(postId);
        return Ok(likes);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromRoute] int postId)
    {
        await _service.Delete(postId);
        return NoContent();
    }
}