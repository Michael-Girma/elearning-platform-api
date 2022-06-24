using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class CreateAssessmentDTO
    {
        [Required]
        public string AssessmentName { get; set;}

        [Required]
        public virtual ICollection<CreateAssessmentQuestionDTO> AssessmentQuestions { get; set; }
    }
}