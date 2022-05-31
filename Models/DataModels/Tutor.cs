using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Tutor : BaseEntity
    {
        [Key]

        public Guid TutorId { get; set; }

        public bool Verified { get; set; } = false;

        [ForeignKey("User")]
        public Guid Uid { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<TaughtSubject> TaughtSubjects { get; set; }
    }
}