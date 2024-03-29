using AutoMapper;
using Cookfy.Entities;
using Cookfy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Cookfy.Services;

public interface IPostService
{
    public Task<List<PostDto>> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize);
    public Task<PostDto> GetPost(int id);
    public Task AddPost(AddPostDto postDto);
    public Task Delete(int id);
    public Task UpdatePost(int id, AddPostDto dto);
    public Task<List<PostDto>> FindByName(string searchName);
    Task<List<PostDto>> GetFavoritesPosts(int pageNumber, int pageSize);
    public Task<List<PostDto>> GetAllFolowed(int pageNumber, int pageSize);
    public Task<List<TrendingPostDto>> GetTrending();
    public Task<List<PostDto>> GetUserPosts(int userId, int pageNumber, int pageSize);
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

    public async Task<List<PostDto>> GetAll(int pageNumber, int pageSize)
    {
       var posts = await _context.Posts.Include(r => r.User).OrderByDescending(t => t.Id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
       var postsDto = _mapper.Map<List<PostDto>>(posts);
       return postsDto;
    }

    public async Task<List<TrendingPostDto>> GetTrending()
    {
        var date = DateTime.Now.AddDays(-7);
        var posts = await _context.Posts.
            Include(r => r.User).
            Include(l => l.Likes).
            Where(r => r.Date >= date)
            .ToListAsync();
        var postsDto = _mapper.Map<List<TrendingPostDto>>(posts).OrderByDescending(l => l.LikeCount).ToList();
        
        var listLength = postsDto.Count;

        if (listLength < 10)
        {
            return postsDto.GetRange(0, listLength);
        }
        else
        {
            return postsDto.GetRange(0 ,10);
        }

    }

    public async Task<List<PostDto>> GetAllFolowed(int pageNumber, int pageSize)
    {
        var followed = await _context.Follows.Where(s => s.FollowerUserId.Equals(_userContextService.GetUserId)).ToListAsync();
        var followedUsersIds = GetFollowedUsersIds(followed);
        var posts = await _context.Posts.Include(r => r.User).
            Where( x => followedUsersIds.Contains(x.UserId)).
            OrderByDescending(t => t.Id).
            Skip(pageSize * (pageNumber-1)).
            Take(pageSize).ToListAsync();
        var postsDto = _mapper.Map<List<PostDto>>(posts);
        return postsDto;
    }

    public async Task<List<PostDto>> GetUserPosts(int userId, int pageNumber, int pageSize)
    {
        var posts = await _context.Posts.Include(r => r.User).
            Where(x => x.UserId == userId).
            OrderByDescending(t => t.Id).
            Skip(pageSize * (pageNumber - 1)).
            Take(pageSize).ToListAsync();
        var postsDto = _mapper.Map<List<PostDto>>(posts);
        return postsDto;
    }

    public async Task<PostDto> GetPost(int id)
    {
        var post = await GetPostById(id);
        var postDto = _mapper.Map<PostDto>(post);
        return postDto;
    }

    public async Task<List<PostDto>> GetFavoritesPosts(int pageNumber, int pageSize)
    {
        var liked = await _context.Likes.Where(s => s.UserId.Equals(_userContextService.GetUserId)).ToListAsync();
        var likedPostsIds = GetFavoritePostsIds(liked);
        var posts = await _context.Posts.Include(r => r.User).
            Where(x => likedPostsIds.Contains(x.Id)).
            OrderByDescending(t => t.Id).
            Skip(pageSize * (pageNumber - 1)).
            Take(pageSize).ToListAsync();
        var postsDto = _mapper.Map<List<PostDto>>(posts);
        return postsDto;
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
        post.Value = dto.Value;
        post.Photo = dto.Photo;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var post = await _context
                .Posts
                .FirstOrDefaultAsync(r => r.Id == id);
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task<List<PostDto>> FindByName(string searchName)
    {
        var posts = await _context.Posts.Include(r => r.User).Where(b => b.Title.Contains(searchName)).ToListAsync();
        var postsDto = _mapper.Map<List<PostDto>>(posts);
        return postsDto;
    }

    private async Task<Post> GetPostById(int id) => await _context.Posts.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);

    private List<int?> GetFavoritePostsIds(List<Like> likes) 
    {
        var results = new List<int?>();
        foreach (var like in likes)
        {
            if (!results.Contains(like.PostId))
            {
                results.Add(like.PostId);
            }
        }
        return results;
    }

    private List<int?> GetFollowedUsersIds(List<Follow> follows)
    {
        var results = new List<int?>();
        foreach (var follow in follows)
        {
            if (!results.Contains(follow.FollowedUserId))
            {
                results.Add(follow.FollowedUserId);
            }
        }
        return results;
    }
}