using Microsoft.EntityFrameworkCore;
using NFLFantasyChallenge.API.DTOs.Auth;
using NFLFantasyChallenge.API.Services.Interfaces;
using NFLFantasyChallenge.Middleware;
using NFLFantasyChallenge.Models;

namespace NFLFantasyChallenge.API.Services;

public class AuthService : IAuthService
{
    private readonly FantasyDbContext _context;
    private readonly IEmailService _emailService;

    public AuthService(FantasyDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task Signup(SignupDTO signupDTO)
    {
        if (await _context.Users.AnyAsync(a => a.Username == signupDTO.Username)
            || await _context.PendingRegistrations.AnyAsync(a => a.Username == signupDTO.Username))
        {
            throw new FantasyAPIException("Username already taken");
        }

        if (await _context.Users.AnyAsync(a => a.Email == signupDTO.Email)
            || await _context.PendingRegistrations.AnyAsync(a => a.Email == signupDTO.Email))
        {
            throw new FantasyAPIException("Email already taken");
        }

        if (signupDTO.Password != signupDTO.ConfirmPassword)
        {
            throw new FantasyAPIException("Passwords do not match");
        }
        var registrationTime = DateTime.Now;

        var newRegistration = new PendingRegistration()
        {
            FullName = signupDTO.FullName,
            Username = signupDTO.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(signupDTO.Password),
            Email = signupDTO.Email,
            RegistrationTime = registrationTime.ToUniversalTime()
        };

        _context.PendingRegistrations.Add(newRegistration);
        await _context.SaveChangesAsync();

        await _emailService.SendNewRegistrationEmail(signupDTO.Username, registrationTime);
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
