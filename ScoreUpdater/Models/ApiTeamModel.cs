namespace ScoreUpdater.Models;

public class ApiTeamModel
{
    public string Team { get; set; }
    public List<ApiTeamPlayerModel> Roster { get; set; }
}

public class ApiTeamPlayerModel
{
    public string LongName { get; set; }
    public string PlayerId { get; set; }
    public string Pos { get; set; }
    public string TeamId { get; set; }
}
