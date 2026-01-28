using Microsoft.EntityFrameworkCore;
using NFLFantasyChallenge.API.DTOs.Auth;
using NFLFantasyChallenge.API.Services.Interfaces;
using NFLFantasyChallenge.Middleware;
using NFLFantasyChallenge.Models;

namespace NFLFantasyChallenge.API.Services;

public class AuthService : IAuthService
{
    private readonly FantasyDbContext _context;

    public AuthService(FantasyDbContext context)
    {
        _context = context;
    }

    public async Task Signup(SignupDTO signupDTO)
    {
        if (await _context.Users.AnyAsync(a => a.Username == signupDTO.Username))
        {
            throw new FantasyAPIException("Username already taken");
        }

        if (signupDTO.Password != signupDTO.ConfirmPassword)
        {
            throw new FantasyAPIException("Passwords do not match");
        }

        var role = await _context.Roles.Where(w => w.RoleName == "Player").FirstOrDefaultAsync();

        if (role == null)
        {
            throw new FantasyAPIException("An Error Occured Signing Up");
        }

        var newUser = new User()
        {
            FullName = signupDTO.FullName,
            Username = signupDTO.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(signupDTO.Password),
            PhoneNumber = signupDTO.Password,
            Role = role
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
    }

    public async Task<UserLoginModel> Login(LoginDTO loginDTO)
    {
        var user = await _context.Users
            .Include(i => i.Role)
            .Where(w => w.Username == loginDTO.Username)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new FantasyAPIException("Invalid Login");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
        {
            throw new FantasyAPIException("Invalid Login");
        }

        var result = new UserLoginModel()
        {
            UserId = user.UserId,
            UserName = user.Username,
            RoleName = user.Role.RoleName
        };

        return result;
    }
}
