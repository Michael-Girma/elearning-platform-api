namespace elearning_platform.DTO
{
    public class CreateChatDTO
    {
        public virtual ICollection<CreateUserChatDTO>? Participants { get; set; }

        public virtual ICollection<CreateChatMessageDTO> Messages { get; set; }
    }
}