using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface IChatRepo
    {
        IEnumerable<Chat> GetChatsForUser(Guid id);
        IEnumerable<User> GetChatParticipants(Guid id);

        IEnumerable<ChatMessage> GetMessagesForChat(Guid id);

        ChatMessage SaveChatMessage(ChatMessage message);
    }
}