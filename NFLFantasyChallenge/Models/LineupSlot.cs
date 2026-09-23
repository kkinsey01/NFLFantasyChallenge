using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NFLFantasyChallenge.Models;

public class LineupSlot
{
    [Key]
    public int LineupSlotId { get; set; }

    [ForeignKey(nameof(Lineup))]
    public int LineupId { get; set; }
    public Lineup Lineup { get; set ; }

    public string Position { get; set; }
    public int SlotIndex { get; set; }

    [ForeignKey(nameof(Player))]
    public int? PlayerId { get; set; }
    public Player Player { get; set; }
}
