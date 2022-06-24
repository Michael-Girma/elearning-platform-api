namespace elearning_platform.DTO
{
    public class CreateAssessmentQuestionDTO
    {
        public string QuestionContent { get; set; }

        public virtual ICollection<CreateQuestionChoiceDTO> Choices { get; set; }
    }
}