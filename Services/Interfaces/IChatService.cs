using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface IChatService
    {
        ChatMessage SendMessage(Guid userId, CreateChatMessageDTO messageDTO);
        Chat CreateMessage(Guid fromUserId, Guid toUserId, CreateChatDTO chatDTO);

    }
}