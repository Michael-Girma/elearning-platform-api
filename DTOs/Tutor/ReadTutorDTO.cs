namespace elearning_platform.DTO
{
    public class ReadTutorDTO
    {
        public Guid TutorId { get; set; }

        public bool Verified { get; set; } = false;
        public Guid Uid { get; set; }

        public ReadUserDTO User { get; set; }
    }
}