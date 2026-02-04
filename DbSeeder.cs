using NFLFantasyChallenge.API.DTOs.JSON;
using NFLFantasyChallenge.Models;
using System.Text.Json;

namespace NFLFantasyChallenge;

public static class DbSeeder
{
    public static void Seed(FantasyDbContext db)
    {
        if (!db.Roles.Any())
        {
            var roles = new List<Role>()
            {
                new Role() {RoleName = "Player"},
                new Role() {RoleName = "Admin"}
            };

            db.Roles.AddRange(roles);
            db.SaveChanges();
        }

        if (!db.Players.Any())
        {
            var json = File.ReadAllText("2025PlayerSource.json");

            var teams = JsonSerializer.Deserialize<Dictionary<string, List<JsonPlayerDTO>>>(json)!;

            foreach (var team in teams.Keys)
            {
                var teamPlayers = teams[team];
                foreach (var player in teamPlayers)
                {
                    var dbPlayer = new Player()
                    {
                        Name = player.Name,
                        Position = player.Position,
                        Team = team,
                        Year = DateTime.Now.Year.ToString()
                    };
                    db.Players.Add(dbPlayer);
                }                
            }
            db.SaveChanges();
        }        
    }
}
