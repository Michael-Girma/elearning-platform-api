using Microsoft.AspNetCore.SignalR;
using elearning_platform.Attributes;
using elearning_platform.Models;
using elearning_platform.Services;
using elearning_platform.ExtensionMethods.Auth;
using AutoMapper;
using elearning_platform.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace elearning_platform.Hubs
{
    [Hub("Notify")]
    public class ChatHub : Hub<IChatHub>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public ChatHub(IHttpContextAccessor contextAccessor,ICurrentUserService currentUserService, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async void SendMessage(User user, ChatMessage message)
        {
            var messageDTO = _mapper.Map<ReadChatMessageDTO>(message);
            await Clients.Groups(message.ChatId.ToString()).SendUpdate(_mapper.Map<ReadUserDTO>(user), messageDTO);
        }

        public override Task OnConnectedAsync()
        {
            var token = _contextAccessor.HttpContext.Request.Query.FirstOrDefault(e=> e.Key == "access_token");
            var tokenString = token.Value;
            if(tokenString == "")
            {
                base.Dispose();
            }
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(tokenString);
            var tokenS = jsonToken as JwtSecurityToken;
            var userData = tokenS.Claims.FirstOrDefault(e => e.Type == ClaimTypes.UserData);
            _currentUserService.SetUser(Guid.Parse(userData?.Value));
            var user = _currentUserService.User;
            var chats = from chat in user.Chats select chat.ChatId.ToString();
            foreach(var chat in chats){
                Groups.AddToGroupAsync(Context.ConnectionId, chat);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}