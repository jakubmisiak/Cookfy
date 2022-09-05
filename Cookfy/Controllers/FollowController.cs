using Cookfy.Entities;
using Cookfy.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cookfy.Controllers;

[Route("api/follow")]
public class FollowController : ControllerBase
{
    private readonly IFollowService _service;

    public FollowController(IFollowService service)
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
    public ActionResult<List<Follow>> Get([FromRoute] int postId)
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