namespace elearning_platform.DTO
{
    public class ReadTaughtSubjectDTO
    {
        public Guid TaughtSubjectId { get; set; }

        public Guid TutorId { get; set; }

        public ReadTutorDTO Tutor { get; set; }

        public Guid SubjectId { get; set; }

        public ReadSubjectDTO Subject { get; set; }

        public string TopicsCovered { get; set; }

        public float PricePerHour { get; set; }

        public bool TaughtOnline { get; set; }

        public bool TaughtInPerson { get; set; }
    }
}