using NFLFantasyChallenge.API.DTOs;

namespace NFLFantasyChallenge.API.Services.Interfaces;

public interface ILeaderboardService
{
    public Task<List<LeaderboardDTO>> GetLeaderboard();
}
