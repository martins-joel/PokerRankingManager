using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokerManager.Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Tournament
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required, MinLength(3), MaxLength(80)]
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfSteps { get; set; }
    public TournamentConfiguration Configuration { get; set; }
    public ICollection<TournamentStep> Steps { get; set; }
}