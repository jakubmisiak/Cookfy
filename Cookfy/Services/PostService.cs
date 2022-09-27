using AutoMapper;
using Cookfy.Entities;
using Cookfy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Cookfy.Services;

public interface IPostService
{
    public Task<List<PostDto>> GetAll();
    public Task<PostDto> GetPost(int id);
    public Task AddPost(AddPostDto postDto);
    public Task Delete(int id);
    public Task UpdatePost(int id, AddPostDto dto);
    public Task<List<PostDto>> FindByName(string searchName);
}

public class PostService : IPostService
{
    private readonly CookfyDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContextService;

    public PostService(CookfyDbContext context, IMapper mapper, IUserContextService userContextService)
    {
        _context = context;
        _mapper = mapper;
        _userContextService = userContextService;
    }

    public async Task<List<PostDto>> GetAll()
    {
       var posts = await _context.Posts.Include(r => r.User).ToListAsync();
       var postsDto = _mapper.Map<List<PostDto>>(posts);
       return postsDto;
    }

    public async Task<PostDto> GetPost(int id)
    {
        var post = await GetPostById(id);
        var postDto = _mapper.Map<PostDto>(post);
        return postDto;
    }

    public async Task AddPost(AddPostDto postDto)
    {
        var post = _mapper.Map<Post>(postDto);
        post.UserId = _userContextService.GetUserId;
        post.Date = DateTime.Now;
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePost(int id, AddPostDto dto)
    {
        var post = await GetPostById(id);
        post.Description = dto.Description;
        post.Ingredient = dto.Ingredient;
        post.Photo = dto.Photo;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var post = await GetPostById(id);
        _context.Posts.Remove(post);
        _context.SaveChangesAsync();
    }

    public async Task<List<PostDto>> FindByName(string searchName)
    {
        var posts = await _context.Posts.Where(b => b.Title.Contains(searchName)).ToListAsync();
        var postsDto = _mapper.Map<List<PostDto>>(posts);
        return postsDto;
    }

    private async Task<Post> GetPostById(int id) => await _context.Posts.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
}