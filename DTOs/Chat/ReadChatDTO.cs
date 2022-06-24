namespace elearning_platform.DTO
{
    public class ReadChatDTO: BaseEntityDTO
    {
        public Guid ChatId { get; set; }

        public Guid InitiatorUid { get; set; }

        public virtual ReadUserDTO Initiator { get; set; }

        public virtual ICollection<ReadUserChatDTO> Participants { get; set; }

        public virtual ICollection<ReadChatMessageDTO> Messages { get; set; }
    }
}