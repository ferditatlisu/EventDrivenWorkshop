using EDCommon;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BFF.Hubs
{
    public class OrderHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendOrder(string connectionId, string message)
        {
            if (Clients != null)
            {
                var client = Clients?.Client(connectionId);
                if (client != null)
                { 
                    await client.SendAsync(CustomKey.SIGNALR_CHECKOUT_METHOD_NAME, connectionId, message);
                }
            }
        }
    }
}
