using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokerManager.Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Player
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required, MinLength(3), MaxLength(80)]
    public string Name { get; set; } = string.Empty;
}
