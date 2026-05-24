using Microsoft.EntityFrameworkCore;

namespace NFLFantasyChallenge.Models;

public class FantasyDbContext : DbContext
{
    public FantasyDbContext(DbContextOptions<FantasyDbContext> options) : base(options)
    {

    }

    public DbSet<ChallengeRule> ChallengeRules { get; set; }
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Lineup> Lineups => Set<Lineup>();
    public DbSet<LineupSlot> Slots => Set<LineupSlot>();
}
