using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NFLFantasyChallenge.Models;

public class Lineup
{
    [Key]
    public int LineupId { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [InverseProperty(nameof(LineupSlot.Lineup))]
    public List<LineupSlot> Slots { get; set; }

    [ForeignKey(nameof(LineupStatus))]
    public int LineupStatusId { get; set; }

    public LineupStatus LineupStatus { get; set; } = null!;
}
