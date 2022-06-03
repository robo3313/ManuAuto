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
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), System.Runtime.Serialization.DataMember]
    public DateTime CreationDate { get; init; } = DateTime.Now;
    [Required]
    public DateTime ModificationDate { get; set; }

    public virtual ICollection<Tag>? Tags { get; set; }
}
