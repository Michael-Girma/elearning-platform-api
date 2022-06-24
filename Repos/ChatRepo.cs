using elearning_platform.Data;
using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public class ChatRepo : IChatRepo
    {
        private readonly AppDbContext _ctx;

        public ChatRepo(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        
        public IEnumerable<User> GetChatParticipants(Guid id)
        {
            var chatUsers = _ctx.UserChats.Where(e => e.ChatId == id);
            var users = from chatUser in chatUsers select chatUser.User;
            return users;
        }

        public IEnumerable<Chat> GetChatsForUser(Guid id)
        {
            var userChats = _ctx.UserChats.Where(e => e.UserId == id);
            var chats = from userChat in userChats select userChat.Chat;
            return chats;
        }

        public IEnumerable<ChatMessage> GetMessagesForChat(Guid id)
        {
            return _ctx.ChatMessages.Where(e => e.ChatId == id);
        }

        public Chat SaveChat(Chat chat)
        {
            _ctx.Chats.Add(chat);
            _ctx.SaveChanges();
            return chat;
        }

        public ChatMessage SaveChatMessage(ChatMessage message)
        {
            _ctx.ChatMessages.Add(message);
            _ctx.SaveChanges();
            return message;
        }
    }
}