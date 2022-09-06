using Cookfy.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cookfy.Services;

public interface ILikeService
{
    public Task Post(int id);
    public Task<List<Like>> Get(int id);
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

    public async Task<List<Like>> Get(int id)
    {
        var likes = await _context.Likes.Where(r => r.PostId == id).ToListAsync();
        return likes;
    }

    public async Task Delete(int postId, int likeId)
    {
        var like = await _context.Likes.FirstOrDefaultAsync(r => r.Id == likeId);
        _context.Likes.Remove(like);
        await _context.SaveChangesAsync();
    }
}