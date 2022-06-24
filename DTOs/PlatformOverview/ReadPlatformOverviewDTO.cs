namespace elearning_platform.DTO
{
    public class ReadPlatformOverviewDTO
    {
        public IEnumerable<ReadUserDTO> DailyUsers { get; set; }

        public IEnumerable<ReadUserDTO> WeeklyUsers { get; set; }

        public IEnumerable<ReadSessionDTO> WeeklySessions { get; set;}

        public IEnumerable<ReadOnlineSessionDTO> WeeklyOnlineSessions { get; set; }

        public float Revenue {get; set;}

        public float WeeklyRevenue { get; set; }

        public IEnumerable<ReadSessionFeedbackDTO> UnaddressedReports { get; set; }

        public IEnumerable<ReadSubjectDTO> Subjects { get; set; }

        public IEnumerable<ReadSubjectRequestDTO> SubjectRequests { get; set; }
    }
}