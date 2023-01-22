using Cookfy.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cookfy.Services;

public interface IFollowService
{
    public Task Post(int id);
    public Task<List<Follow>> Get(int id);
    public Task Delete(int postId, int followId);
}

public class FollowService : IFollowService
{
    private readonly CookfyDbContext _context;
    private readonly IUserContextService _userContextService;
    
    public FollowService(CookfyDbContext context, IUserContextService userContextService)
    {
        _context = context;
        _userContextService = userContextService;
    }

    public async Task Post(int id)
    {
        var follow = new Follow()
        {
            FollowedUserId = id,
            FollowerUserId = _userContextService.GetUserId
        };
        _context.Follows.Add(follow);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Follow>> Get(int id)
    {
        var follows = await _context.Follows.Where(r => r.FollowedUserId == id).ToListAsync();
        return follows;
    }

    public async Task Delete(int postId, int followId)
    {
        var follows = await _context.Follows.FirstOrDefaultAsync(r => r.FollowedUserId == followId);
        _context.Follows.Remove(follows);
        await _context.SaveChangesAsync();
    }
}