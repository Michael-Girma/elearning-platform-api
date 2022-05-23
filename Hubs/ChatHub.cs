using Microsoft.AspNetCore.SignalR;
using elearning_platform.Attributes;
using elearning_platform.Models;
using elearning_platform.Services;
using Microsoft.AspNetCore.Authorization;
using elearning_platform.ExtensionMethods.Auth;

namespace elearning_platform.Hubs
{
    [Hub("Notify")]
    [Authorize]
    public class ChatHub : Hub<IChatHub>
    {
        private readonly ICurrentUserService _currentUserService;

        public ChatHub(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async void SendMessage(string user, ChatMessage message)
        {
            // var message = 
            throw new Exception("WOY");
            await Clients.All.SendUpdate(user, message);
        }

        public override Task OnConnectedAsync()
        {
            _currentUserService.SetUser((Guid)Context.User.GetUserId());
            var user = _currentUserService.User;
            var chats = new string[]{user.Username};
            foreach(var chat in chats){
                Groups.AddToGroupAsync(Context.ConnectionId, chat);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var user = _currentUserService.User;
            Console.WriteLine(user.ToString());
            return base.OnDisconnectedAsync(exception);
        }
    }
}