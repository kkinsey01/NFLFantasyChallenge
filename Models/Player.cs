using System.ComponentModel.DataAnnotations;

namespace NFLFantasyChallenge.Models;

public class Player
{
    [Key]
    public int PlayerId { get; set; }

    public string Name { get; set; }
    public string Team { get; set; }
    public string Position { get; set; }
    public string Year { get; set; }
    public double? WildcardScore { get; set; }
    public double? DivisionalScore { get; set; }
    public double? ConferenceScore { get; set; }
    public double? SuperBowlScore { get; set; }
}
