using System.Security.Cryptography.X509Certificates;
using Cookfy.Models;
using Cookfy.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cookfy.Controllers;

[Route("api/post/{postId}/comment")]
public class CommentController : ControllerBase
{
    public readonly ICommentService _service;

    public CommentController(ICommentService service)
    {
        _service = service;
    }

    [HttpPost]
    public ActionResult Post([FromRoute]int postId, [FromBody]AddCommentDto dto)
    {
        _service.Post(postId, dto);
        return Ok();
    }

    [HttpGet]
    public ActionResult<List<CommentDto>> Post([FromRoute]int postId)
    {
        var comments = _service.Get(postId);
        return Ok(comments);
    }

    [HttpDelete("{commentId}")]
    public ActionResult Delete([FromRoute] int postId, [FromRoute] int commentId)
    {
        _service.Delete(postId, commentId);
        return NoContent();
    }
}