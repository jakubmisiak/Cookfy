using Cookfy.Models;
using Cookfy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Cookfy.Controllers;

[Route("api/post")]
[ApiController]
[Authorize]
public class PostController : ControllerBase
{
    private readonly IPostService _service;

    public PostController(IPostService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var posts = await _service.GetAll(pageNumber, pageSize);
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetPost([FromRoute]int id)
    {
        var post = await _service.GetPost(id);
        return Ok(post);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> AddPost([FromBody] AddPostDto postDto)
    {
        await _service.AddPost(postDto);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public async Task<ActionResult> UpdatePost([FromRoute]int id,[FromBody] AddPostDto postDto)
    {
        await _service.UpdatePost(id, postDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute]int id)
    {
        await _service.Delete(id);
        return Ok();
    }
    
    [HttpGet("search/{title}")]
    public async Task<ActionResult<List<PostDto>>> GetByTitle([FromRoute]string title)
    {
        var dtos = await _service.FindByName(title);
        return Ok(dtos);
    }


    [HttpGet("favorites")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetFavPosts([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var posts = await _service.GetFavoritesPosts(pageNumber, pageSize);
        return Ok(posts);
    }

    [HttpGet("follow")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetFollowedPosts([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var posts = await _service.GetAllFolowed(pageNumber, pageSize);
        return Ok(posts);
    }

    [HttpGet("trending")]
    public async Task<ActionResult<IEnumerable<TrendingPostDto>>> GetTrendingPosts()
    {
        var posts = await _service.GetTrending();
        return Ok(posts);
    }
}