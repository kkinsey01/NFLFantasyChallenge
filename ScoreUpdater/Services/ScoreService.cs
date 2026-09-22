using ScoreUpdater.Models;

namespace ScoreUpdater.Services;

public class ScoreService
{
    private readonly ExternalApiClient _externalApiClient;

    public ScoreService(ExternalApiClient externalApiClient)
    {
        _externalApiClient = externalApiClient;
    }

    public async Task<List<ApiGameModel>> GetWeeksGames(string week)
    {       
        var response = await _externalApiClient.GetAsync<ApiResponse<List<ApiGameModel>>>(
            "getNFLGamesForWeek",
            new Dictionary<string, string>()
            {
                ["week"] = week,
                ["seasonType"] = "post",
                ["season"] = "2025"
            });

        if (response == null)
        {
            throw new Exception("Error getting games for week " + week);
        }

        var weeksGames = response.Body;

        return weeksGames;
    }

    public async Task<List<ApiTeamPlayerModel>> GetTeamsPlayers(string teamAbv)
    {
        var response = await _externalApiClient.GetAsync<ApiResponse<ApiTeamModel>>(
            "getNFLTeamRoster",
            new Dictionary<string, string>()
            {
                ["teamAbv"] = teamAbv
            });

        if (response == null)
        {
            throw new Exception("Error getting players for team " + teamAbv);
        }

        var teamModel = response.Body;

        var teamsPlayers = teamModel.Roster;

        return teamsPlayers;
    }
}
