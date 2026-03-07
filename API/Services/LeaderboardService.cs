using Microsoft.EntityFrameworkCore;
using NFLFantasyChallenge.API.DTOs;
using NFLFantasyChallenge.API.Services.Interfaces;
using NFLFantasyChallenge.Models;

namespace NFLFantasyChallenge.API.Services;

public class LeaderboardService : ILeaderboardService
{
    private readonly FantasyDbContext _context;

    public LeaderboardService(FantasyDbContext context)
    {
        _context = context;
    }

    public async Task<List<LeaderboardDTO>> GetLeaderboard()
    {
        var lineups = await _context.Lineups
            .Include(i => i.User)
            .Include(i => i.Slots)
            .ThenInclude(t => t.Player)
            .ToListAsync();

        var result = new List<LeaderboardDTO>();

        foreach (var lineup in lineups)
        {
            var totalScore = lineup.Slots.Sum(s => 
                (s.Player.WildcardScore ?? 0) +
                (s.Player.DivisionalScore ?? 0) + 
                (s.Player.ConferenceScore ?? 0) + 
                (s.Player.SuperBowlScore ?? 0)
            );

            result.Add(new LeaderboardDTO()
            {
                UserFullName = lineup.User.FullName,
                TotalScore = Math.Round(totalScore, 2)
            });
        }

        result = result.OrderByDescending(s => s.TotalScore).ToList();

        for (int i = 0; i < result.Count; i++)
        {
            result[i].Rank = i + 1;
        }

        return result;
    }
}
