using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadTutorDetailsDTO
    {
        public Guid TutorId { get; set; }

        public bool Verified { get; set; } = false;
        
        public ReadUserDTO User { get; set; }

        public IEnumerable<Session> Sessions { get; set; }

        public bool TeachesOnline { get; set; }
    }
}