using NFLFantasyChallenge.API.DTOs.Admin;
using NFLFantasyChallenge.API.DTOs.Admin.EditScores;
using NFLFantasyChallenge.API.DTOs.Admin.ManageUsers;
using NFLFantasyChallenge.API.DTOs.Lineup;

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
    public Task<List<ManageUserDTO>> GetUserBalances();
    public Task UpdateUserBalance(ManageUserDTO manageBalanceDTO);
    public Task<List<PendingRegistrationDTO>> GetPendingRegistrations();
    public Task ApprovePendingRegistration(int registrationId);
    public Task DenyPendingRegistration(int registrationId);
    public Task<string> UpdateScoresForWeek(string Week);
    public Task<List<LineupViewDTO>> GetLineupForApproval(int userId);
    public Task ApproveLineup(int userId);
    public Task DenyLineup(int userId);
}
