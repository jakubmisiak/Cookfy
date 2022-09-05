using Cookfy.Entities;

namespace Cookfy.Services;

public interface ILikeService
{
    public void Post(int id);
    public List<Like> Get(int id);
    public void Delete(int postId, int likeId);
}

public class LikeService : ILikeService
{
    private readonly CookfyDbContext _context;
    
    public LikeService(CookfyDbContext context)
    {
        _context = context;
    }

    public void Post(int id)
    {
        var like = new Like()
        {
            PostId = id,
            UserId = 1
        };
        _context.Likes.Add(like);
        _context.SaveChanges();
    }

    public List<Like> Get(int id)
    {
        var likes = _context.Likes.Where(r => r.PostId == id).ToList();
        return likes;
    }

    public void Delete(int postId, int likeId)
    {
        var like = _context.Likes.FirstOrDefault(r => r.Id == likeId);
        _context.Likes.Remove(like);
        _context.SaveChanges();
    }
}