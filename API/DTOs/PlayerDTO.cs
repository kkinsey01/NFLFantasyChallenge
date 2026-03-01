namespace NFLFantasyChallenge.API.DTOs;

public class PlayerDTO
{
    public int PlayerID { get; set; }
    public string PlayerName { get; set; }
    public string Position { get; set; }
    public string Team { get; set; }

    public string DisplayName => $"{PlayerName} ({Team})";
}
