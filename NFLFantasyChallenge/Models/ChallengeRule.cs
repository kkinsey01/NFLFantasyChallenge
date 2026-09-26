using System.ComponentModel.DataAnnotations;

namespace NFLFantasyChallenge.Models;

public class ChallengeRule
{
    [Key]
    public int ChallengeRuleId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
