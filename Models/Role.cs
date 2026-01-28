using System.ComponentModel.DataAnnotations;

namespace NFLFantasyChallenge.Models;

public class Role
{
    [Key]
    public int RoleId { get; set; }

    public string RoleName { get; set; }
}
