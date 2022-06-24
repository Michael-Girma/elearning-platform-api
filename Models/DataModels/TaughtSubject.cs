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

        public virtual Tutor Tutor { get; set; }

        [Required]
        [ForeignKey("Subject")]
        public Guid SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public string TopicsCovered { get; set; }

        public float PricePerHour { get; set; }

        public bool Approved { get; set; }

        public bool TaughtOnline { get; set; }

        public bool TaughtInPerson { get; set; }

        public int PreferredSessionLength { get; set; }
        public int PreferredSessionCount { get; set; }

        public bool Deleted { get; set; }

        public virtual IEnumerable<LessonDocument> LessonDocuments { get; set; }
    }
}