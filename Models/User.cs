using System.ComponentModel.DataAnnotations;

namespace NFLFantasyChallenge.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string? PhoneNumber { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
