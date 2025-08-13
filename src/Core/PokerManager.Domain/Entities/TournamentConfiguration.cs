using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokerManager.Domain.Entities;

public class TournamentConfiguration
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public long TournamentId { get; set; }

    [ForeignKey(nameof(TournamentId))]
    public Tournament Tournament { get; set; }

    // How many worst scores to remove from the final ranking
    public int CutCount { get; set; }

    // Score system configuration (e.g., points for each position)
    [Required]
    public Dictionary<int, int> PositionScores { get; set; } = new();
}