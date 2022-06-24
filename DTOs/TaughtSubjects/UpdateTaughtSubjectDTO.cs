namespace elearning_platform.DTO
{
    public class UpdateTaughtSubjectDTO
    {
        public bool TaughtInPerson { get; set; }

        public bool TaughtOnline { get; set; }

        public int PreferredSessionCount { get; set; }

        public int PreferredSessionLength { get; set; }

        public string TopicsCovered { get; set; }
        public float PricePerHour { get; set; }

        public IEnumerable<CreateLessonDocumentDTO> LessonDocuments { get; set; }
    }
}