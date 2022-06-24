using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadChatMessageDTO: BaseEntityDTO
    {
        public Guid MessageId { get; set; }
        public string Message { get; set; }

        public bool SeenByParticipant { get; set; }

        public virtual IEnumerable<InternalFileMetadata> Attachments { get; set; }

        public Guid SenderId { get; set; }

        public virtual ReadUserDTO Sender { get; set; }

        public Guid ChatId { get; set; }

        public virtual ReadChatDTO Chat { get; set; }
    }
}