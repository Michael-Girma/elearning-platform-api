using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Exceptions;
using elearning_platform.Models;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepo _chatRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public ChatService(IChatRepo chatRepo, IMapper mapper, IUserRepo userRepo)
        {
            _chatRepo = chatRepo;
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public ChatMessage SendMessage(Guid userId, CreateChatMessageDTO messageDTO)
        {
            var message = _mapper.Map<ChatMessage>(messageDTO);
            var userExists = _chatRepo.GetChatParticipants(message.ChatId).Any(e => e.Uid == userId);
            // if(userExists)
            // {

            // }else{
            //     return 
            // }
            return message;
        }

        public Chat CreateMessage(Guid fromUserId, Guid toUserId, CreateChatDTO chatDTO)
        {
            chatDTO.Participants = new CreateUserChatDTO[]{
                new CreateUserChatDTO() { UserId = toUserId },
                new CreateUserChatDTO() { UserId = fromUserId }
            };
            var chat = _mapper.Map<Chat>(chatDTO);
            chat.InitiatorUid = fromUserId;
            foreach(var message in chat.Messages)
            {
                message.SenderId = fromUserId;
                message.SeenByParticipant = false;
            }
            var exstingChat = _chatRepo.GetChatsForUser(fromUserId).ToList().FirstOrDefault(e => e.Participants.Any(e => e.UserId == toUserId));
            if(exstingChat != null)
            {
                throw new BadRequestException("A chat already exists");
            }
            _chatRepo.SaveChat(chat);
            foreach(var participant in chat.Participants)
            {
                participant.User = _userRepo.GetUserById(participant.UserId);
            }
            return chat;
        }
    }
}