using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepo _chatRepo;
        private readonly IMapper _mapper;

        public ChatService(IChatRepo chatRepo, IMapper mapper)
        {
            _chatRepo = chatRepo;
            _mapper = mapper;
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
    }
}