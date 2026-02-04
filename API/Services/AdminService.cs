using Microsoft.EntityFrameworkCore;
using NFLFantasyChallenge.API.DTOs.Admin.EditScores;
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
}
