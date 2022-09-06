using AutoMapper;
using Cookfy.Entities;
using Cookfy.Models;

namespace Cookfy;

public class CookfyMappingProfile : Profile
{
    public CookfyMappingProfile()
    {
        CreateMap<RegisterUserDto, UserDto>();
        CreateMap<User, UserDto>();
        CreateMap<AddPostDto, Post>();
        CreateMap<Post, PostDto>();
        CreateMap<AddCommentDto, Comment>();
        CreateMap<Comment, CommentDto>();
        CreateMap<LoginDto, User>();
    }
}