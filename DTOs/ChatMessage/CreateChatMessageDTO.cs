namespace elearning_platform.DTO
{
    public class CreateChatMessageDTO
    {
        public string Message { get; set; }

        public virtual IEnumerable<CreateMessageAttachmentDTO> Attachments { get; set; }

        public Guid ChatId { get; set; }
    }
}