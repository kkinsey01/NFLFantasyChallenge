namespace ScoreUpdater.Models;

public class ApiFantasyTeamScoreModel
{
    public ApiFantasyScoreModel GameInfo { get; set; }
}

public class ApiFantasyScoreModel
{
    public string FantasyPoints { get; set; }
    public ApiFantasyPointsDefault FantasyPointsDefault { get; set; }
}