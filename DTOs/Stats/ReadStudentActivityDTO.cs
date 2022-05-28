namespace elearning_platform.DTO
{
    public class ReadStudentActivityDTO
    {
        public ICollection<ReadTutorRequestDTO> TutorRequests { get; set; }

        public string[] StarredSubjects { get; set; } = new string[]{};

        public ICollection<ReadSessionDTO> Sessions { get; set; }

        public string[] Feedbacks { get; set; } = new string[]{};
    }
}