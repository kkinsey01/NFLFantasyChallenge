using Microsoft.EntityFrameworkCore;

namespace NFLFantasyChallenge.Models;

public class FantasyDbContext : DbContext
{
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Lineup> Lineups => Set<Lineup>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=fantasy.db");
    }
}
