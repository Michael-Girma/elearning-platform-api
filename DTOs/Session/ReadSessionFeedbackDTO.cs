namespace elearning_platform.DTO
{
    public class ReadSessionFeedbackDTO: BaseEntityDTO
    {
        public Guid Id { get; set; }

        public int Stars { get; set; }

        public string Comments { get; set; }

        public bool Report { get; set; }

        public Guid SessionId { get; set; }

        public Guid StudentId { get; set; }

        public ReadStudentDTO Student { get; set;}

        public string ReportAddressed { get; set;}

        public Guid TutorId { get; set; }

        public virtual ReadTutorDTO Tutor { get; set; }

        public virtual ReadSessionDTO Session { get; set; }
    }
}