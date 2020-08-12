using Common.data;
using Common.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSocketsServer.Hubs
{
    public class TopPerformersHub : Hub<ITopPerformersClient>
    {
        private string senderId;
        private string receiverId;
        public async Task UpdateTopPerformers(List<Performer> topPerformers)
        {
            if (receiverId != null)
            {
                await Clients.Client(receiverId).UpdateBoard(topPerformers);
            }
        }

      /*  public override async Task OnConnectedAsync()
        {

            if (Context.User.IsInRole("sender"))
            {
                this.senderId = Context.ConnectionId;
            }
            else if (Context.User.IsInRole("receiver"))
            {
                this.receiverId = Context.ConnectionId;
            }

            await base.OnConnectedAsync();
        } */

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (receiverId != null && Context.ConnectionId.CompareTo(receiverId) == 0)
                receiverId = null;
            if (senderId != null && Context.ConnectionId.CompareTo(senderId) == 0)
                senderId = null;


            await base.OnDisconnectedAsync(exception);
        }

        public void  RegisterAsReceiver()
        {
            this.receiverId = Context.ConnectionId;
            Console.WriteLine($"Registered {Context.ConnectionId} as receiver");
        }

        public void RegisterAsSender()
        {
            this.senderId = Context.ConnectionId;
            Console.WriteLine($"Registered {Context.ConnectionId} as sender");
        }

    }
}
