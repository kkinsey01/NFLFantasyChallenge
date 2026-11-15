using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NFLFantasyChallenge.Models;

public class LineupStatus
{
    [Key]
    public int LineupStatusId { get; set; }

    public string StatusName { get; set; }
}
