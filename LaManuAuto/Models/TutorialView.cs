using System.ComponentModel.DataAnnotations;

namespace LaManuAuto.Models
{
    public class TutorialView
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TutorialId { get; set; }
    }
}
