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
    public ActionResult Post([FromRoute] int postId)
    {
        _service.Post(postId);
        return Ok();
    }

    [HttpGet]
    public ActionResult<List<Like>> Get([FromRoute] int postId)
    {
        var likes = _service.Get(postId);
        return Ok(likes);
    }

    [HttpDelete("{likeId}")]
    public ActionResult Delete([FromRoute] int postId, [FromRoute] int likeId)
    {
        _service.Delete(postId, likeId);
        return NoContent();
    }
}