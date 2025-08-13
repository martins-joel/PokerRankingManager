using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokerManager.Domain.Entities;

public class TournamentStepResult
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public long TournamentStepId { get; set; }

    [ForeignKey(nameof(TournamentStepId))]
    public TournamentStep TournamentStep { get; set; }

    [Required]
    public long PlayerId { get; set; }

    [ForeignKey(nameof(PlayerId))]
    public Player Player { get; set; }

    [Required]
    public int Position { get; set; }
}