using AutoMapper;
using Cookfy.Entities;
using Cookfy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cookfy.Services;

public interface ICommentService
{
    public Task Post(int postId, AddCommentDto dto);
    public Task<List<CommentDto>> Get(int postId, int pageNumber, int pageSize);
    public Task Delete(int postId,int commentId);
    public Task<List<CommentDto>> GetUserComments( int pageNumber, int pageSize);
}

public class CommentService : ICommentService
{
    private readonly CookfyDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContextService;
    public CommentService(CookfyDbContext dbContext, IMapper mapper, IUserContextService userContextService)
    {
        _context = dbContext;
        _mapper = mapper;
        _userContextService = userContextService;
    }

    public async Task Post(int postId, AddCommentDto dto)
    {
        var comment = _mapper.Map<Comment>(dto);
        comment.UserId = _userContextService.GetUserId;
        comment.Date = DateTime.Now;
        comment.PostId = postId;
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CommentDto>> Get(int postId, int pageNumber, int pageSize)
    {
        var coments = await _context.Comments.Include(r => r.User)
            .Where(r => r.PostId == postId).
            OrderByDescending(t => t.Id).
            Skip(pageSize * (pageNumber - 1)).
            Take(pageSize).ToListAsync();

        var comentsDto = _mapper.Map<List<CommentDto>>(coments);
        return comentsDto;
    }

    public async Task Delete(int postId,int commentId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(r => r.Id == postId);
        var comment = await _context.Comments.FirstOrDefaultAsync(r => r.Id == commentId);
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CommentDto>> GetUserComments(int pageNumber, int pageSize)
    {
        var coments = await _context.Comments.Include(r => r.User)
            .Where(r => r.UserId == _userContextService.GetUserId).
            OrderByDescending(t => t.Id).
            Skip(pageSize * (pageNumber - 1)).
            Take(pageSize).ToListAsync();

            var comentsDto = _mapper.Map<List<CommentDto>>(coments);
            return comentsDto;
    }
}