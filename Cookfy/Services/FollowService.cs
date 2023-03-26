using Cookfy.Entities;
using Cookfy.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookfy.Services;

public interface IFollowService
{
    public Task Post(int userId);
    public Task<FollowDto> Get(int userId);
    public Task Delete(int userId);
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

    public async Task Post(int userId)
    {
        var follow = new Follow()
        {
            FollowedUserId = userId,
            FollowerUserId = _userContextService.GetUserId
        };
        _context.Follows.Add(follow);
        await _context.SaveChangesAsync();
    }

    public async Task<FollowDto> Get(int userId)
    {
        var followsCount = await _context.Follows.Where(r => r.FollowedUserId == userId).CountAsync();
        var currentUserFollows = await _context.Follows.AnyAsync(r => r.FollowedUserId == userId && r.FollowerUserId == _userContextService.GetUserId);
        var follows = new FollowDto()
        {
            FollowsCount = followsCount,
            CurrentUserFollows = currentUserFollows
        };
        return follows;
    }

    public async Task Delete(int userId)
    {
        var follows = await _context.Follows.FirstOrDefaultAsync(r => r.FollowedUserId == userId && r.FollowerUserId == _userContextService.GetUserId);
        _context.Follows.Remove(follows);
        await _context.SaveChangesAsync();
    }
}