using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NFLFantasyChallenge.Models;

public class Lineup
{
    [Key]
    public int LineupId { get; set; }

    [ForeignKey(nameof(Qb1))]
    public int QbId1 { get; set; }
    public Player Qb1 { get; set; } = null!;

    [ForeignKey(nameof(Qb2))]
    public int QbId2 { get; set;}
    public Player Qb2 { get; set; } = null!;

    [ForeignKey(nameof(Rb1))]
    public int RbId1 { get; set; }
    public Player Rb1 { get; set; } = null!;

    [ForeignKey(nameof(Rb2))]
    public int RbId2 { get; set; } 
    public Player Rb2 { get; set; } = null!;

    [ForeignKey(nameof(Rb3))]
    public int RbId3 { get; set; }
    public Player Rb3 { get; set; } = null!;

    [ForeignKey(nameof(Rb4))]
    public int RbId4 { get; set; }
    public Player Rb4 { get; set; } = null!;

    [ForeignKey(nameof(Rb5))]
    public int RbId5 { get; set; }
    public Player Rb5 { get; set; } = null!;

    [ForeignKey(nameof(Wr1))]
    public int WrId1 { get; set; }
    public Player Wr1 { get; set; } = null!;

    [ForeignKey(nameof(Wr2))]
    public int WrId2 { get; set; }
    public Player Wr2 { get; set; } = null!;

    [ForeignKey(nameof(Wr3))]
    public int WrId3 { get; set; }
    public Player Wr3 { get; set; } = null!;

    [ForeignKey(nameof(Wr4))]
    public int WrId4 { get; set; }
    public Player Wr4 { get; set; } = null!;

    [ForeignKey(nameof(Wr5))]
    public int WrId5 { get; set; }
    public Player Wr5 { get; set; } = null!;

    [ForeignKey(nameof(Te1))]
    public int TeId1 { get; set; }
    public Player Te1 { get; set; } = null!;

    [ForeignKey(nameof(Te2))]
    public int TeId2 { get; set; }
    public Player Te2 { get; set; } = null!;

    [ForeignKey(nameof(Kicker))]
    public int KickerId { get; set; }
    public Player Kicker { get; set; } = null!;

    [ForeignKey(nameof(Defense))]
    public int DefenseId { get; set; }
    public Player Defense { get; set; } = null!;

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
