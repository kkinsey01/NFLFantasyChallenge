namespace NFLFantasyChallenge.PageModels;

public class LineupViewModel
{   
    public List<LineupPlayers> Players { get; set; }
    public bool IsApproval { get; set; }
}

public class LineupPlayers
{
    public string Position { get; set; }
    public string PlayerName { get; set; }
    public string Team { get; set; }
    public double TotalScore { get; set; }

    public string DisplayTotalScore => TotalScore.ToString("F2");
}