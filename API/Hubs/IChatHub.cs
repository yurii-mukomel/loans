using API.Models;
using System.Threading.Tasks;

namespace API.Hubs
{
    public interface IChatHub
    {
        Task SendMessage(string message);
        Task JoinRoom(UserConnection userConnection);
        Task SendConnectedUsers(string room);
        Task SendDisconnected(string key);
    }
}