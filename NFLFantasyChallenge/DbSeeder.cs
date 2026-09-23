using NFLFantasyChallenge.API.DTOs.JSON;
using NFLFantasyChallenge.Models;
using ScoreUpdater.Services;
using System.Text.Json;

namespace NFLFantasyChallenge;

public class DbSeeder
{
    private readonly ScoreService _scoreService;

    public DbSeeder(ScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    public async Task Seed(FantasyDbContext db)
    {
        if (!db.Roles.Any())
        {
            var roles = new List<Role>()
            {
                new Role() {RoleName = "Player"},
                new Role() {RoleName = "Admin"},
                new Role() {RoleName = "DevAdmin"}
            };

            db.Roles.AddRange(roles);
            db.SaveChanges();
        } 

        if (!db.Players.Any())
        {
            await AddPlayers(db);
        }        

        if (!db.ChallengeRules.Any())
        {
            var challengeRules = new List<ChallengeRule>()
            {
                new ChallengeRule() { Name = "QbCount", Description = "2" },
                new ChallengeRule() { Name = "RbCount", Description = "5" },
                new ChallengeRule() { Name = "WrCount", Description = "5" },
                new ChallengeRule() { Name = "TeCount", Description = "2" },
                new ChallengeRule() { Name = "KCount", Description = "1" },
                new ChallengeRule() { Name = "DCount", Description = "1" }
            };

            db.ChallengeRules.AddRange(challengeRules);
            db.SaveChanges();
        }
    }

    private async Task AddPlayers(FantasyDbContext db)
    {
        var positions = new List<string>() { "QB", "RB", "WR", "TE", "PK" };

        var teamAbbreviations = new List<string>() { "DEN" };

        //var teamAbbreviations = new List<string>() { "DEN", "PIT", "HOU", "JAX", "BUF", "NE", "LAC",
        //                                 "SEA", "CAR", "LAR", "PHI", "SF", "CHI", "GB" };

        foreach (var teamAbv in teamAbbreviations)
        {
            var teamPlayers = await _scoreService.GetTeamsPlayers(teamAbv);
            foreach (var position in positions)
            {
                var playersForPosition = teamPlayers
                    .Where(w => w.Pos == position)
                    .OrderBy(o => o.LongName)
                    .ToList();

                foreach (var player in playersForPosition)
                {
                    var newPlayer = new Player()
                    {
                        Name = player.LongName,
                        Team = teamAbv,
                        Position = position,
                        Year = DateTime.Now.Year.ToString(),
                        WildcardScore = 0,
                        DivisionalScore = 0,
                        ConferenceScore = 0,
                        SuperBowlScore = 0,
                        RapidApiPlayerId = int.TryParse(player.PlayerId, out var apiPlayerId) ? apiPlayerId : 0,
                        RapidApiTeamId = int.TryParse(player.TeamId, out var apiTeamId) ? apiTeamId : 0
                    };

                    db.Players.Add(newPlayer);
                }
            }
        }

        await db.SaveChangesAsync();
    }
}
