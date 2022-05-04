using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class TutorRequest : BaseEntity
    {
        [Key]
        public Guid TutorRequestId { get; set; }

        [Required]
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        [ForeignKey("TaughtSubject")]
        public Guid TaughtSubjectId { get; set; }
        public TaughtSubject TaughtSubject { get; set; }

        [Required]
        public string Status { get; set; }
        //AWAITING_STUDENT || AWAITING_TUTOR || ACCEPTED
        [Required]
        public bool OnlineSession { get; set; }

        [Required]
        public int SessionLength { get; set; }

        [Required]
        public DateTime[] PreferredDates { get; set; }

        [Required]
        public string Note { get; set; }

        public ICollection<OnlineSession> OnlineSessions { get; set; }
    }
}