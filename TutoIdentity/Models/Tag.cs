using System.ComponentModel.DataAnnotations;

namespace ManuAuto.Models;

public class Tag
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(20)]
    public string Shortname { get; set; }
    [Required, MaxLength(100)]
    public string Fullname { get; set; }

    public virtual ICollection<Tutorial>? Tutorials { get; set; }
}

