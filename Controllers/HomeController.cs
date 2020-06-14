using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalRTestApp.Models;

namespace SignalRTestApp.Controllers
{
    public class HomeController : Controller
    {
        IHubContext<ChatHub> hubContext;
        public HomeController(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task Create(string connectionId, string user, string message)
        {
            Message msg1 = new Message()
            {
                UserName = user,
                Text = message,
                Class = "text-dark"
            };
            Message msg2 = new Message()
            {
                UserName = user,
                Text = message,
                Class = "text-light bg-dark"
            };

            await hubContext.Clients.AllExcept(connectionId).SendAsync("Notify", msg1);
            await hubContext.Clients.Client(connectionId).SendAsync("Notify", msg2);
        }
    }
}
