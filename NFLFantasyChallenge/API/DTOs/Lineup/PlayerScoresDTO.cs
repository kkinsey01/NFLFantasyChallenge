namespace NFLFantasyChallenge.API.DTOs.Lineup;

public class PlayerScoresDTO
{
    public string PlayerName { get; set; }
    public string Team { get; set; }
    public double WildCardScore { get; set; }
    public double DivisionalScore { get; set; }
    public double ConferenceScore { get; set; }
    public double SuperBowlScore { get; set; }
    public double TotalScore { get; set; }
}
