using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("{processRequestId}")]
        public async Task<IActionResult> Send(Guid processRequestId)
        {
            await this.context.Clients.All.InvokeAsync("notificationArrived", processRequestId);
            return Ok();
        }
    }
}
