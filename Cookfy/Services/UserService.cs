using AutoMapper;
using Cookfy.Entities;
using Cookfy.Models;

namespace Cookfy.Services;

public interface IUserService
{
    public void RegisterUser(RegisterUserDto dto);
    public List<UserDto> GetAll();
    public UserDto GetUser(int id);
    public void DeleteUser(int id);
    public void UpdateUser(int id, RegisterUserDto dto);
}

public class UserService : IUserService
{
    private readonly CookfyDbContext _context;
    private readonly IMapper _mapper;
    public UserService(CookfyDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public void RegisterUser(RegisterUserDto dto)
    {
        var newUser = new User()
        {
            UserName = dto.UserName,
            Password = dto.Password,
            Description = dto.Description,
            Photo = dto.Photo
        };
        _context.Users.Add(newUser);
        _context.SaveChanges();
    }

    public List<UserDto> GetAll()
    {
        var users = _context.Users.ToList();
        var userDtos = _mapper.Map<List<UserDto>>(users);
        return userDtos;
    }

    public UserDto GetUser(int id)
    {
        var user = GetUserById(id);
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public void DeleteUser(int id)
    {
        var user = GetUserById(id);
        _context.Remove(user);
        _context.SaveChanges();
    }

    public void UpdateUser(int id, RegisterUserDto dto)
    {
        var user = GetUserById(id);

        user.UserName = dto.UserName;
        user.Password = dto.Password;
        user.Photo = dto.Photo;
        user.Description = dto.Description;


        _context.SaveChanges();
    }

    private User GetUserById(int id) => _context.Users.FirstOrDefault(r => r.Id == id);
}