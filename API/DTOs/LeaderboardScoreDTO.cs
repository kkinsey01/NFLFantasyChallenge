namespace NFLFantasyChallenge.API.DTOs;

public class LeaderboardScoreDTO
{
    public int UserId { get; set; }
    public int Rank { get; set; }
    public string UserFullName { get; set; }
    public double TotalScore { get; set; }
}
