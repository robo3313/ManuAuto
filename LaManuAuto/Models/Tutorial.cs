using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaManuAuto.Models;

public class Tutorial
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string? Title { get; set; }
    [Required, MaxLength(1000)]
    public string? Description { get; set; }
    [Required, MaxLength(100)]
    public string? VideoUrl { get; set; }
    [Required]
    public DateTime CreationDate { get; set; }
    [Required]
    public DateTime ModificationDate { get; set; }
    public virtual ICollection<Tag>? Tags { get; set; }
    public virtual ICollection<TutorialView>? TutorialViews { get; set; }

    [NotMapped]
    public bool Viewed { get; set; } = false;
}
