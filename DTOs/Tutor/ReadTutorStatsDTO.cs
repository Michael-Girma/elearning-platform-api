namespace elearning_platform.DTO
{
    public class ReadTutorStatsDTO 
    {
        public IEnumerable<ReadTutorRequestDTO> TutorRequests { get; set; }

        public IEnumerable<ReadSessionDTO> Sessions { get; set; }

        public float Revenue { get; set; }

        public IEnumerable<ReadSessionFeedbackDTO> Feedbacks { get; set; }
    }
}