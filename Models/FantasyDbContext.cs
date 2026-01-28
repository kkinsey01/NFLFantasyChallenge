using Microsoft.EntityFrameworkCore;

namespace NFLFantasyChallenge.Models;

public class FantasyDbContext : DbContext
{
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=fantasy.db");
    }
}
