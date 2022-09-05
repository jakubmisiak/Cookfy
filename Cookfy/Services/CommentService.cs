using AutoMapper;
using Cookfy.Entities;
using Cookfy.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookfy.Services;

public interface ICommentService
{
    public void Post(int postId, AddCommentDto dto);
    public List<CommentDto> Get(int postId);
    public void Delete(int postId,int commentId);
}

public class CommentService : ICommentService
{
    private readonly CookfyDbContext _context;
    private readonly IMapper _mapper;
    public CommentService(CookfyDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public void Post(int postId, AddCommentDto dto)
    {
        var comment = _mapper.Map<Comment>(dto);
        comment.Date = DateTime.Now;
        comment.PostId = postId;
        _context.Comments.Add(comment);
        _context.SaveChanges();
    }

    public List<CommentDto> Get(int postId)
    {
        var coments = _context.Comments.Include(r => r.User)
            .Where(r => r.PostId == postId).ToList();
        var comentsDto = _mapper.Map<List<CommentDto>>(coments);
        return comentsDto;
    }

    public void Delete(int postId,int commentId)
    {
        var post = _context.Posts.FirstOrDefault(r => r.Id == postId);
        var comment = _context.Comments.FirstOrDefault(r => r.Id == commentId);
        _context.Comments.Remove(comment);
        _context.SaveChanges();
    }
}