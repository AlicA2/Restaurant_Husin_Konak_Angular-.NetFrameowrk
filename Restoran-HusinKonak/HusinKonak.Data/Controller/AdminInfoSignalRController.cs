using HusinKonak.Data.Hub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminInfoSignalRController : ControllerBase
    {
        private IHubContext<MessageHub, IMessageHubClient> messageHub;
        private readonly RestaurantDBContext _dbContext;

        public AdminInfoSignalRController(RestaurantDBContext dbContext, IHubContext<MessageHub, IMessageHubClient> _messageHub)
        {
            messageHub = _messageHub;
            this._dbContext = dbContext;
        }



        [HttpPost]
        public ActionResult Get()
        {
            List<string> data = new List<string>();
            data.Add("Broj korisnika: " + _dbContext.Korisnik.Count().ToString());
            data.Add("Broj admina " + _dbContext.Admin.Count().ToString());
            messageHub.Clients.All.SendToAdmin(data);
            return Ok();
        }
    }
}
