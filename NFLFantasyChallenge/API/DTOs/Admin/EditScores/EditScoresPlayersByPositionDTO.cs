using NFLFantasyChallenge.API.Enums;

namespace NFLFantasyChallenge.API.DTOs.Admin.EditScores;

public class EditScoresPlayersByPositionDTO
{    
    public PlayerPosition Position { get; set; }
    public List<EditScoresPlayerDTO> Players { get; set; }

    public string PositionName => Position.ToString();
}
