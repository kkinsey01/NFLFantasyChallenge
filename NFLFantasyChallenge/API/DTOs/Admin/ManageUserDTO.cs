namespace NFLFantasyChallenge.API.DTOs.Admin;

public class ManageUserDTO
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public double Balance { get; set; }
    public string? LineupStatus { get; set; }
}
