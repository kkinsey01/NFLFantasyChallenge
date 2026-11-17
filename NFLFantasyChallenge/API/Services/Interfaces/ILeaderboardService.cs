using NFLFantasyChallenge.API.DTOs;
using NFLFantasyChallenge.API.DTOs.Lineup;

namespace NFLFantasyChallenge.API.Services.Interfaces;

public interface ILeaderboardService
{
    public Task<LeaderboardDTO> GetLeaderboard(int userId);
    public Task<List<LineupViewDTO>> ViewLineup(int userId);
}
