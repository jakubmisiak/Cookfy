using AutoMapper;
using Cookfy.Entities;
using Cookfy.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookfy.Services;

public interface IPostService
{
    public List<PostDto> GetAll();
    public PostDto GetPost(int id);
    public void AddPost(AddPostDto postDto);
    public void Delete(int id);
    public void UpdatePost(int id, AddPostDto dto);
}

public class PostService : IPostService
{
    private readonly CookfyDbContext _context;
    private readonly IMapper _mapper;

    public PostService(CookfyDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<PostDto> GetAll()
    {
       var posts = _context.Posts.Include(r => r.User).ToList();
       var postsDto = _mapper.Map<List<PostDto>>(posts);
       return postsDto;
    }

    public PostDto GetPost(int id)
    {
        var post = GetPostById(id);
        var postDto = _mapper.Map<PostDto>(post);
        return postDto;
    }

    public void AddPost(AddPostDto postDto)
    {
        var post = _mapper.Map<Post>(postDto);
        post.Date = DateTime.Now;
        _context.Posts.Add(post);
        _context.SaveChanges();
    }

    public void UpdatePost(int id, AddPostDto dto)
    {
        var post = GetPostById(id);
        post.Description = dto.Description;
        post.Ingredient = dto.Ingredient;
        post.Photo = dto.Photo;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var post = GetPostById(id);
        _context.Posts.Remove(post);
        _context.SaveChanges();
    }

    private Post GetPostById(int id) => _context.Posts.Include(r => r.User).FirstOrDefault(r => r.Id == id);
}