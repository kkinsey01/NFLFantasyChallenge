using NFLFantasyChallenge.API.DTOs;

namespace NFLFantasyChallenge.API.Services.Interfaces;

public interface ILeaderboardService
{
    public Task<LeaderboardDTO> GetLeaderboard(int userId);
}
