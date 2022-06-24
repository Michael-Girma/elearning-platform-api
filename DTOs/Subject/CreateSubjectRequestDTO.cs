namespace elearning_platform.DTO
{
    public class CreateSubjectRequestDTO
    {
        public string SubjectName { get; set; }

        public Guid EducationLevelId { get; set;}

        public string Description { get; set; }
    }
}