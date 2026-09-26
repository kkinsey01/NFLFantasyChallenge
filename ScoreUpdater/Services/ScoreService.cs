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

    public async Task<double> GetScoresForTeamForWeek(string gameId, string teamId)
    {
        var response = await _externalApiClient.GetAsync<ApiResponse<Dictionary<string, ApiPlayerScoreModel>>>(
            "getNFLGamesForPlayer", 
            new Dictionary<string, string>()
            {
                ["teamID"] = teamId,
                ["gameID"] = gameId,
                ["fantasyPoints"] = "true"
            });

        if (response == null)
        {
            throw new Exception($"Error getting updated scores for team {teamId}");
        }

        var gameInfo = response.Body.Values.First();

        var fantasyPoints = gameInfo.FantasyPointsDefault.PPR;

        return double.TryParse(fantasyPoints, out var result) ? result : 0;
    }

    public async Task<double> GetScoresForPlayerForWeek(string gameId, string playerId)
    {
        var response = await _externalApiClient.GetAsync<ApiResponse<Dictionary<string, ApiPlayerScoreModel>>>(
            "getNFLGamesForPlayer",
            new Dictionary<string, string>()
            {
                ["gameID"] = gameId,
                ["playerID"] = playerId,
                ["fantasyPoints"] = "true",                
            });

        if (response == null)
        {
            throw new Exception($"Error getting updated scores for player {playerId}");
        }

        // this should only ever return one result, since we can only query by one player, and we'll query by one game at a time
        var gameInfo = response.Body.Values.First();

        var fantasyPoints = gameInfo.FantasyPointsDefault.PPR;
        
        return double.TryParse(fantasyPoints, out var result) ? result : 0;
    }
}
