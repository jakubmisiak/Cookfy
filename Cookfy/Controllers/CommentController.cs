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
    public async Task<ActionResult> Post([FromRoute]int postId, [FromBody]AddCommentDto dto)
    {
        await _service.Post(postId, dto);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<CommentDto>>> Post([FromRoute]int postId)
    {
        var comments = await _service.Get(postId);
        return Ok(comments);
    }

    [HttpDelete("{commentId}")]
    public async Task<ActionResult> Delete([FromRoute] int postId, [FromRoute] int commentId)
    {
        await _service.Delete(postId, commentId);
        return NoContent();
    }
}