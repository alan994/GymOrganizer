using BusinessLogic.Model;
using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Web.Hubs;

namespace Web.Controllers
{
    [Route("api/notifications")]
    public class NotificationController : Controller
    {
        private readonly IHubContext<NotificationHub> context;

        public NotificationController(IHubContext<NotificationHub> context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] QueueResult queueResult)
        {
            await this.context.Clients.All.InvokeAsync("notificationArrived", queueResult);
            return Ok();
        }
    }
}
