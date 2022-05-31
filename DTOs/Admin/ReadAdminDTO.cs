namespace elearning_platform.DTO
{
    public class ReadAdminDTO
    {
        public Guid AdminId { get; set; }

        public Guid Uid { get; set; }

        public ReadUserDTO User { get; set; }
    }
}