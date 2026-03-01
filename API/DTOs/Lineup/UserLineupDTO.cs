namespace NFLFantasyChallenge.API.DTOs.Lineup;

public class UserLineupDTO
{
    public string FullName { get; set; }  
    public bool IsLocked { get; set; }
    public List<LineupPlayersByPosition> PositionGroups { get; set; }
}
