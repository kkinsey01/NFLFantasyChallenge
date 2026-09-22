using ScoreUpdater.Models;
using ScoreUpdater.Services;

namespace ScoreUpdater;

public class Program
{
    static async Task Main(string[] args)
    {
        //var httpClient = new HttpClient()
        //{
        //    BaseAddress = new Uri("https://tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com/")
        //};

        //var apiClient = new ExternalApiClient(httpClient);

        //var scoreService = new ScoreService(apiClient);

        ////var weeksGames = await scoreService.GetWeeksGames("1");

        ////foreach (var game in weeksGames)
        ////{
        ////    Console.WriteLine($"GameId: {game.GameId} AwayTeam: {game.Away} HomeTeam: {game.Home}");
        ////}

        ////Console.WriteLine("\n\n\n");

        //var teamsPlayers = await scoreService.GetTeamsPlayers("NE");

        //var positions = new List<string> { "QB", "RB", "WR", "TE" };
        //var offense = teamsPlayers.Where(w => positions.Contains(w.Pos)).ToList();

        //foreach (var player in offense) 
        //{
        //    Console.WriteLine($"{player.LongName} {player.PlayerId}");
        //}
    }
}
