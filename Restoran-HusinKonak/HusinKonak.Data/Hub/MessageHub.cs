using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Hub
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        public async Task SendToAdmin(List<string> message)
        {
            await Clients.All.SendToAdmin(message);
        }
    }
}