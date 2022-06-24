using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class AssessmentQuestion : BaseEntity
    {
        [Key]
        public Guid QuestionId { get; set; }

        public string QuestionContent { get; set; }

        [ForeignKey("Assessment")]
        public Guid AssessmentId { get; set; }

        public virtual Assessment Assessment { get; set; }

        public virtual ICollection<QuestionChoice> Choices { get; set; }
    }
}