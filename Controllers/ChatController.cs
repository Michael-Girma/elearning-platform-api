using elearning_platform.Hubs;
using elearning_platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace elearning_platform.Controllers
{
    [Route("Messages")]
    [ApiController]
    public class ChatController: ControllerBase
    { 
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub, IChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public ActionResult SendMessage()
        {
            var message = new ChatMessage(){
                // Content = "sup boii", 
                Sender = "MEEEEEE"
            };
            _hubContext.Clients.Groups("Yaay").SendUpdate("Yayyy", message);
            return Ok("Sent");
        }
    }
}