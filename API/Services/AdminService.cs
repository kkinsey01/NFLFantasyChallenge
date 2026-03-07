using Microsoft.EntityFrameworkCore;
using NFLFantasyChallenge.API.DTOs.Admin.EditScores;
using NFLFantasyChallenge.API.DTOs.Admin.ManageUsers;
using NFLFantasyChallenge.API.DTOs.Auth;
using NFLFantasyChallenge.API.Enums;
using NFLFantasyChallenge.API.Services.Interfaces;
using NFLFantasyChallenge.Middleware;
using NFLFantasyChallenge.Models;

namespace NFLFantasyChallenge.API.Services;

public class AdminService : IAdminService
{
    private readonly FantasyDbContext _context;

    public AdminService(FantasyDbContext context)
    {
        _context = context;
    }

    public async Task<EditScoresDropdownDTO> GetEditScoresDropdownInfo()
    {
        var result = new EditScoresDropdownDTO();

        var dbTeams = await _context.Players
                                    .Select(s => s.Team)
                                    .Distinct()
                                    .OrderBy(o => o)
                                    .ToListAsync();

        result.Teams = dbTeams;

        var weeks = new List<string>()
        {
            "Wildcard",
            "Divisional",
            "Conference",
            "Super Bowl"
        };

        result.Weeks = weeks;

        return result;
    }

    public async Task<List<EditScoresPlayersByPositionDTO>> GetEditScoresPlayerInfo(string Team, string Week)
    {
        var teamPlayers = await _context.Players
                                        .Where(w => w.Team == Team)
                                        .ToListAsync();

        var result = new List<EditScoresPlayersByPositionDTO>();

        var positionOrder = new List<string>{ "QB", "RB", "WR", "TE", "K", "D" };
        var positions = teamPlayers.Select(s => s.Position)
                                   .Distinct()                                   
                                   .OrderBy(o => positionOrder.IndexOf(o))
                                   .ToList();

        foreach (var position in positions)
        {
            var playersByPosition = teamPlayers.Where(w => w.Position == position).ToList();

            result.Add(new EditScoresPlayersByPositionDTO()
            {
                Position = Enum.Parse<PlayerPosition>(position),
                Players = playersByPosition
                    .Select(s => new EditScoresPlayerDTO()
                    {
                        PlayerId = s.PlayerId,
                        PlayerName = s.Name,
                        Score = GetScore(s, Week)
                    })
                    .ToList()
            });
        }                

        return result;
    }

    private double? GetScore(Player player, string week)
    {
        if (week.Equals("Wildcard", StringComparison.CurrentCultureIgnoreCase))
        {
            return player.WildcardScore;
        }
        if (week.Equals("Divisional", StringComparison.CurrentCultureIgnoreCase))
        {
            return player.DivisionalScore;
        }
        if (week.Equals("Conference", StringComparison.CurrentCultureIgnoreCase))
        {
            return player.ConferenceScore;
        }
        if (week.Equals("Super Bowl", StringComparison.CurrentCultureIgnoreCase))
        {
            return player.SuperBowlScore;
        }

        return null;
    }

    public async Task EditPlayerScores(AllEditScoresPlayersDTO allPlayers)
    {
        var team = allPlayers.Team;
        var week = allPlayers.Week;

        var playerIds = allPlayers.Players.Select(s => s.PlayerId).ToList();
        var dbPlayers = await _context.Players
                                      .Where(w => playerIds.Contains(w.PlayerId))
                                      .ToListAsync();

        foreach (var player in allPlayers.Players)
        {
            var dbPlayer = dbPlayers.Where(w => w.PlayerId == player.PlayerId).FirstOrDefault();
            if (dbPlayer == null)
            {
                throw new FantasyAPIException("Invalid Player");
            }
            if (week == "Wildcard")
            {
                dbPlayer.WildcardScore = player.NewScore;
            }
            else if (week == "Divisional")
            {
                dbPlayer.DivisionalScore = player.NewScore;
            }
            else if (week == "Conference")
            {
                dbPlayer.ConferenceScore = player.NewScore;
            }
            else if (week == "Super Bowl")
            {
                dbPlayer.SuperBowlScore = player.NewScore;
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<RoleDTO>> GetRoles()
    {
        return await _context.Roles
            .Select(s => new RoleDTO()
            {
                RoleID = s.RoleId,
                RoleName = s.RoleName
            })
            .ToListAsync();
    }

    public async Task<List<UserDTO>> GetUsers()
    {
        return await _context.Users
            .Select(s => new UserDTO()
            {
                UserID = s.UserId,
                UserName = s.Username,
                Password = "",
                FullName = s.FullName,
                PhoneNumber = s.PhoneNumber ?? "",
                Balance = s.Balance,
                RoleID = s.RoleId,
                RoleName = s.Role.RoleName,
            })
            .OrderBy(o => o.FullName)
            .ToListAsync();
    }

    public async Task UpdateUser(UserDTO user)
    {
        var dbUser = await _context.Users
            .Include(i => i.Role)
            .Where(w => w.UserId == user.UserID)
            .FirstOrDefaultAsync();

        if (dbUser == null)
        {
            throw new FantasyAPIException("Invalid User");
        }

        if (dbUser.Username != user.UserName)
        {
            if (await _context.Users.AnyAsync(a => a.Username == user.UserName))
            {
                throw new FantasyAPIException("Username already taken");
            }
            dbUser.Username = user.UserName;
        }

        if (!string.IsNullOrEmpty(user.Password))
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            dbUser.Password = hashedPassword;
        }

        if (dbUser.FullName != user.FullName)
        {
            dbUser.FullName = user.FullName;
        }

        if (dbUser.PhoneNumber != user.PhoneNumber)
        {
            dbUser.PhoneNumber = user.PhoneNumber;
        }

        if (dbUser.Balance != user.Balance)
        {
            dbUser.Balance = user.Balance;
        }

        if (dbUser.RoleId != user.RoleID)
        {
            dbUser.RoleId = user.RoleID;
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(int userId)
    {
        var dbUser = await _context.Users.FindAsync(userId);

        if (dbUser == null)
        {
            throw new FantasyAPIException("Invalid User");
        }

        var userLineups = await _context.Lineups.Where(w => w.UserId == userId).ToListAsync();

        if (userLineups.Count != 0)
        {
            foreach (var lineup in userLineups)
            {
                _context.Lineups.Remove(lineup);
            }
        }

        _context.Users.Remove(dbUser);

        await _context.SaveChangesAsync();
    }
}
