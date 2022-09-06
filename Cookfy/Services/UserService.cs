using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Azure;
using Cookfy.Entities;
using Cookfy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Cookfy.Services;

public interface IUserService
{
    public Task RegisterUser(RegisterUserDto dto);
    public Task<List<UserDto>> GetAll();
    public Task<UserDto> GetUser(int id);
    public Task DeleteUser(int id);
    public Task UpdateUser(int id, RegisterUserDto dto);

    public Task<string> GenerateJwt(LoginDto dto);
}

public class UserService : IUserService
{
    private readonly CookfyDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;
    public UserService(CookfyDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
    {
        _context = dbContext;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
    }

    public async Task RegisterUser(RegisterUserDto dto)
    {
        var newUser = new User()
        {
            UserName = dto.UserName,
            Description = dto.Description,
            Photo = dto.Photo
        };
        var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
        newUser.Password = hashedPassword;
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
    }

    public async Task<List<UserDto>> GetAll()
    {
        var users = await _context.Users.ToListAsync();
        var userDtos = _mapper.Map<List<UserDto>>(users);
        return userDtos;
    }

    public async Task<UserDto> GetUser(int id)
    {
        var user = await GetUserById(id);
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public async Task DeleteUser(int id)
    {
        var user = await GetUserById(id);
        _context.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(int id, RegisterUserDto dto)
    {
        var user = await GetUserById(id);

        user.UserName = dto.UserName;
        user.Password = dto.Password;
        user.Photo = dto.Photo;
        user.Description = dto.Description;


        await _context.SaveChangesAsync();
    }

    public async Task<string> GenerateJwt(LoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == dto.UserName);
        
        if (user is null)
        {
            throw new Exception("Invalid username or password");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
        
        if (result == PasswordVerificationResult.Failed)
        {
            throw new Exception("Invalid username or password");
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.UserName}"),
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private async Task<User> GetUserById(int id) => await _context.Users.FirstOrDefaultAsync(r => r.Id == id);
}