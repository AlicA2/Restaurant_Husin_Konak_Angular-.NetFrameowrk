using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Hub
{
    public interface IMessageHubClient
    {
        Task SendToAdmin(List<string> message);
    }
}
