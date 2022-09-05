using AutoMapper;
using Cookfy.Entities;
using Cookfy.Models;

namespace Cookfy;

public class CookfyMappingProfile : Profile
{
    public CookfyMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<AddPostDto, Post>();
        CreateMap<Post, PostDto>();
    }
}