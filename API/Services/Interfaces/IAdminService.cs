using NFLFantasyChallenge.API.DTOs.Admin.EditScores;

namespace NFLFantasyChallenge.API.Services.Interfaces;

public interface IAdminService
{
    public Task<EditScoresDropdownDTO> GetEditScoresDropdownInfo();

    public Task<List<EditScoresPlayersByPositionDTO>> GetEditScoresPlayerInfo(string Team, string Week);

    public Task EditPlayerScores(AllEditScoresPlayersDTO allPlayers);
}
