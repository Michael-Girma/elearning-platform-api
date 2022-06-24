using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Hubs
{
    public interface IChatHub
    {
        Task SendUpdate(ReadUserDTO user, ReadChatMessageDTO message);
    }
}