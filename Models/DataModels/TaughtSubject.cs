using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class TaughtSubject : BaseEntity
    {
        [Key]
        public Guid TaughtSubjectId { get; set; }

        [Required]
        [ForeignKey("Tutor")]
        public Guid TutorId { get; set; }

        public Tutor Tutor { get; set; }

        [Required]
        [ForeignKey("Subject")]
        public Guid SubjectId { get; set; }

        public Subject Subject { get; set; }

        public string TopicsCovered { get; set; }

        public float PricePerHour { get; set; }

        public bool TaughtOnline { get; set; }

        public bool TaughtInPerson { get; set; }
    }
}