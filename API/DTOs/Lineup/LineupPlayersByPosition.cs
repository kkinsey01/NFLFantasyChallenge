using NFLFantasyChallenge.API.DTOs.Admin.EditScores;
using NFLFantasyChallenge.API.Enums;

namespace NFLFantasyChallenge.API.DTOs.Lineup;

public class LineupPlayersByPosition
{
    public PlayerPosition Position { get; set; }
    public List<LineupSlotDTO> Players { get; set; }

    public string PositionName => Position.ToString();
}
