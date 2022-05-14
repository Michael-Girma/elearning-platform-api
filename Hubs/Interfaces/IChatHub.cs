using elearning_platform.Models;

namespace elearning_platform.Hubs
{
    public interface IChatHub
    {
        Task SendUpdate(string user, ChatMessage message);
    }
}