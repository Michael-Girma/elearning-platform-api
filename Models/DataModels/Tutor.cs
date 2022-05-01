using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Tutor
    {
        [Key]

        public Guid tutorId { get; set; }

        public bool verified { get; set; } = false;

        [ForeignKey("User")]
        public Guid Uid { get; set; }

        public User User { get; set; }
    }
}