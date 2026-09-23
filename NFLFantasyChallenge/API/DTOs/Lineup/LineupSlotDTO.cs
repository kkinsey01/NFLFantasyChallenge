namespace NFLFantasyChallenge.API.DTOs.Lineup;

public class LineupSlotDTO
{
    public int PlayerId { get; set ; }
    public int SlotIndex { get; set; }
    public string Position { get; set; }
    public string PlayerName { get; set; }    
    public string Team { get; set; }
}
