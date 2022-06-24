namespace elearning_platform.DTO
{
    public class ReadUserChatDTO: BaseEntityDTO
    {
        public Guid UserId { get; set; }

        public Guid ChatId { get; set; }

        public virtual ReadUserDTO User { get; set; }
        public virtual ReadChatDTO Chat { get; set; }
    }
}