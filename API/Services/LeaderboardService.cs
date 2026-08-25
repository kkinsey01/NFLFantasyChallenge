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

    public async Task<LeaderboardDTO> GetLeaderboard(int userId)
    {
        var result = new LeaderboardDTO();        

        var lineups = await _context.Lineups
            .Include(i => i.User)
            .Include(i => i.Slots)
            .ThenInclude(t => t.Player)
            .ToListAsync();        

        var scores = new List<LeaderboardScoreDTO>();

        foreach (var lineup in lineups)
        {
            var totalScore = lineup.Slots.Sum(s => 
                (s.Player.WildcardScore ?? 0) +
                (s.Player.DivisionalScore ?? 0) + 
                (s.Player.ConferenceScore ?? 0) + 
                (s.Player.SuperBowlScore ?? 0)
            );

            scores.Add(new LeaderboardScoreDTO()
            {
                UserId = lineup.UserId,
                UserFullName = lineup.User.FullName,
                TotalScore = Math.Round(totalScore, 2)
            });
        }

        scores = scores.OrderByDescending(s => s.TotalScore).ToList();

        for (int i = 0; i < scores.Count; i++)
        {
            scores[i].Rank = i + 1;
        }

        var userScore = scores.Where(w => w.UserId == userId).FirstOrDefault();
        if (userScore != null)
        {
            result.UserStanding = $"{GetOrdinal(userScore.Rank)} - {userScore.TotalScore} total points";
        }
        else
        {
            result.UserStanding = "N/A";
        }

        result.Scores = scores;

        return result;
    }

    private string GetOrdinal(int index)
    {
        if (index % 100 is >= 11 and <= 13)
            return $"{index}th";

        return (index % 10) switch
        {
            1 => $"{index}st",
            2 => $"{index}nd",
            3 => $"{index}rd",
            _ => $"{index}th"
        };
    }
}
