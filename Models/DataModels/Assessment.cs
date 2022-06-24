using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Assessment : BaseEntity
    {
        [Key]
        public Guid AssessmentId { get; set; }

        public string AssessmentName { get; set;}

        public int? LastScore { get; set; }

        [ForeignKey("Session")]
        public Guid SessionId { get; set; }

        public virtual ICollection<AssessmentQuestion> AssessmentQuestions { get; set; }
    }
}