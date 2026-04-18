using NFLFantasyChallenge.API.DTOs.Admin;
using NFLFantasyChallenge.API.DTOs.Admin.EditScores;
using NFLFantasyChallenge.API.DTOs.Admin.ManageUsers;

namespace NFLFantasyChallenge.API.Services.Interfaces;

public interface IAdminService
{
    public Task<EditScoresDropdownDTO> GetEditScoresDropdownInfo();

    public Task<List<EditScoresPlayersByPositionDTO>> GetEditScoresPlayerInfo(string Team, string Week);

    public Task EditPlayerScores(AllEditScoresPlayersDTO allPlayers);
    public Task<List<RoleDTO>> GetRoles();
    public Task<List<UserDTO>> GetUsers();
    public Task UpdateUser(UserDTO user);
    public Task DeleteUser(int userId);
    public Task<List<ManageBalanceDTO>> GetUserBalances();
    public Task UpdateUserBalance(ManageBalanceDTO manageBalanceDTO);
}
