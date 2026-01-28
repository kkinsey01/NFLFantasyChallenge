using NFLFantasyChallenge.Models;

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
    }
}
