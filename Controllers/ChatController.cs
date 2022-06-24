using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Hubs;
using elearning_platform.Models;
using elearning_platform.Repo;
using elearning_platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace elearning_platform.Controllers
{
    [Route("Chats")]
    [ApiController]
    public class ChatController: ControllerBase
    { 
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;
        private readonly IChatRepo _chatRepo;
        private readonly IChatService _chatService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public ChatController(IMapper mapper, IHubContext<ChatHub, IChatHub> hubContext, IChatRepo chatRepo, IChatService chatService, ICurrentUserService currentUserService)
        {
            _hubContext = hubContext;
            _chatRepo = chatRepo;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _chatService = chatService;
        }

        [HttpPost]
        [Authorize]
        [Route("{chatId}")]
        public ActionResult SendMessage(Guid chatId, CreateChatMessageDTO chatMessageDTO)
        {
            var user = _currentUserService.User;
            var message = _mapper.Map<ChatMessage>(chatMessageDTO);
            message.ChatId = chatId;
            message.SenderId = user.Uid;
            message.SeenByParticipant = false;
            _chatRepo.SaveChatMessage(message);
            _hubContext.Clients.Groups(chatMessageDTO.ChatId.ToString()).SendUpdate(_mapper.Map<ReadUserDTO>(user), _mapper.Map<ReadChatMessageDTO>(message));
            return Ok(_mapper.Map<ReadChatMessageDTO>(message));
        }

        [HttpGet]
        [Authorize]
        [Route("all")]
        public ActionResult GetAllChats()
        {
            var user = _currentUserService.User;
            var chats = _mapper.Map<IEnumerable<ReadChatDTO>>(_chatRepo.GetChatsForUser(user.Uid).ToList());
            return Ok(chats);
        }

        [HttpGet]
        [Authorize]
        [Route("with_user/{userId}")]
        public ActionResult GetChatWithUser(Guid userId)
        {
            var user = _currentUserService.User;
            var chats = _mapper.Map<ReadChatDTO>(_chatRepo.GetChatsForUser(user.Uid).ToList().FirstOrDefault(e => e.Participants.FirstOrDefault(d => d.UserId == userId) != null));
            return Ok(chats);
        }

        [HttpGet]
        [Authorize]
        [Route("chat_id/{chatId}")]
        public ActionResult GetChatByChatId(Guid chatId)
        {
            var user = _currentUserService.User;
            var chats = _mapper.Map<ReadChatDTO>(_chatRepo.GetChatsForUser(user.Uid).FirstOrDefault(e => e.ChatId == chatId));
            return Ok(chats);
        }

        [HttpPost]
        [Authorize]
        [Route("create/{toUserId}")]
        public ActionResult CreateChat(Guid toUserId, CreateChatDTO chatDTO)
        {
            var user = _currentUserService.User;
            var chats = _mapper.Map<ReadChatDTO>(_chatService.CreateMessage(user.Uid, toUserId, chatDTO));
            return Ok(chats);
        }
    }
}