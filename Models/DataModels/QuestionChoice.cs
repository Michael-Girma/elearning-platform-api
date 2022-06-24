using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class QuestionChoice : BaseEntity
    {
        [Key]
        public Guid QuestionChoiceId { get; set; }

        [ForeignKey("AssessmentQuestion")]
        public Guid AssessmentQuestionId { get; set; }

        public virtual AssessmentQuestion AssessmentQuestion { get; set; }

        public string Explanation { get; set; }

        public string Content { get; set; }

        public bool IsCorrectAnswer { get; set; }
    }
}