using NFLFantasyChallenge.API.DTOs;
using NFLFantasyChallenge.API.DTOs.Lineup;

namespace NFLFantasyChallenge.API.Services.Interfaces;

public interface ILineupControlService
{
    public Task<UserLineupDTO> GetUserLineup(int userId);
    public Task<List<PlayerDTO>> GetPlayerList(int userId, string Position);
    public Task AddPlayerToLineup(int userId, int PlayerId);
    public Task RemovePlayerFromLineup(int userId, int PlayerId);
    public Task LockLineup(int userId);
    public Task<PlayerScoresDTO> GetIndividualScores(int playerId);
}
