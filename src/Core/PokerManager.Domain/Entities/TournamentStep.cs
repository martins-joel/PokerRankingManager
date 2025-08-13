using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokerManager.Domain.Entities;

public class TournamentStep
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public long TournamentId { get; set; }

    [ForeignKey(nameof(TournamentId))]
    public Tournament Tournament { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public ICollection<TournamentStepResult> Results { get; set; } = new List<TournamentStepResult>();
}