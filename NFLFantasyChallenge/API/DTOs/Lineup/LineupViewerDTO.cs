namespace NFLFantasyChallenge.API.DTOs.Lineup;

public class LineupViewerDTO
{
    public int DefaultUserId { get; set; } = 0;
    public List<UserLineupInfoDTO> UserLineupInfoList { get; set; } = new();
}
