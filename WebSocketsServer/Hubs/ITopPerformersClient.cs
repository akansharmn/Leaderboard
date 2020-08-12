using Common.data;
using Common.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebSocketsServer.Hubs
{
    public interface ITopPerformersClient
    {
        Task UpdateBoard(List<Performer> topPerformers);
    }
}