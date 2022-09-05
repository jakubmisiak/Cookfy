using Cookfy.Models;
using Cookfy.Services;
using Microsoft.AspNetCore.Mvc;
namespace Cookfy.Controllers;

[Route("api/post")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _service;

    public PostController(IPostService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PostDto>> GetAll()
    {
        var posts = _service.GetAll();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public ActionResult<PostDto> GetPost([FromRoute]int id)
    {
        var post = _service.GetPost(id);
        return Ok(post);
    }

    [HttpPost]
    public ActionResult AddPost([FromBody] AddPostDto postDto)
    {
        _service.AddPost(postDto);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public ActionResult UpdatePost([FromRoute]int id,[FromBody] AddPostDto postDto)
    {
        _service.UpdatePost(id, postDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute]int id)
    {
        _service.Delete(id);
        return Ok();
    }
}