using Cookfy.Entities;
using Cookfy.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookfy.Services;

public interface ILikeService
{
    public Task Post(int id);
    public Task<LikeDto> Get(int id);
    public Task Delete(int postId, int likeId);
}

public class LikeService : ILikeService
{
    private readonly CookfyDbContext _context;
    private readonly IUserContextService _userContextService;
    public LikeService(CookfyDbContext context, IUserContextService userContextService)
    {
        _context = context;
        _userContextService = userContextService;
    }

    public async Task Post(int id)
    {
        var like = new Like()
        {
            PostId = id,
            UserId = _userContextService.GetUserId
        };
        _context.Likes.Add(like);
        await _context.SaveChangesAsync();
    }

    public async Task<LikeDto> Get(int id)
    {
        var likesCount = await _context.Likes.Where(r => r.PostId == id).CountAsync();
        var currentUserLike = await _context.Likes.AnyAsync(r => r.PostId == id && r.UserId == _userContextService.GetUserId);
        var likes = new LikeDto()
        {
            LikesCount = likesCount,
            CurrentUserLike = currentUserLike
        };
        return likes;
    }

    public async Task Delete(int postId, int likeId)
    {
        var like = await _context.Likes.FirstOrDefaultAsync(r => r.Id == likeId);
        _context.Likes.Remove(like);
        await _context.SaveChangesAsync();
    }
}