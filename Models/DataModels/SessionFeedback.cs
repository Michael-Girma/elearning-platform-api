using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class SessionFeedback : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public int Stars { get; set; }

        public string Comments { get; set; }

        public bool Report { get; set; }
        public bool ReportAddressed { get; set; } = false;

        [ForeignKey("Session")]
        public Guid SessionId { get; set; }

        [ForeignKey("Tutor")]
        public Guid TutorId { get; set; }

        [ForeignKey("Student")]
        public Guid StudentId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Session Session { get; set; }

        public virtual Tutor Tutor { get; set; }
    }
}