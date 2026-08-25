namespace NFLFantasyChallenge.API.DTOs;

public class LeaderboardDTO
{
    public string UserStanding { get; set; }
    public List<LeaderboardScoreDTO> Scores { get; set; }
}
