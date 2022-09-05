using Cookfy.Entities;

namespace Cookfy.Services;

public interface IFollowService
{
    public void Post(int id);
    public List<Follow> Get(int id);
    public void Delete(int postId, int followId);
}

public class FollowService : IFollowService
{
    private readonly CookfyDbContext _context;
    
    public FollowService(CookfyDbContext context)
    {
        _context = context;
    }

    public void Post(int id)
    {
        var follow = new Follow()
        {
            FollowedUserId = id,
            FollowingUserId = 1
        };
        _context.Follows.Add(follow);
        _context.SaveChanges();
    }

    public List<Follow> Get(int id)
    {
        var follows = _context.Follows.Where(r => r.FollowedUserId == id).ToList();
        return follows;
    }

    public void Delete(int postId, int followId)
    {
        var follows = _context.Follows.FirstOrDefault(r => r.FollowedUserId == followId);
        _context.Follows.Remove(follows);
        _context.SaveChanges();
    }
}