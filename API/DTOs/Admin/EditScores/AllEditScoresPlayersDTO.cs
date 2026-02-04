namespace NFLFantasyChallenge.API.DTOs.Admin.EditScores;

public class AllEditScoresPlayersDTO
{
    public string Team { get; set; }
    public string Week { get; set; }
    public List<EditPlayerScoreDTO> Players { get; set; }
}
