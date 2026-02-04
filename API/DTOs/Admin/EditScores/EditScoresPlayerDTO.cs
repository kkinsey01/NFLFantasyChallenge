namespace NFLFantasyChallenge.API.DTOs.Admin.EditScores;

public class EditScoresPlayerDTO
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }
    public double? Score { get; set; }
}
