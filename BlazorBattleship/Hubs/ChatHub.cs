using Microsoft.AspNetCore.SignalR;
namespace BlazorBattleship.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendCoordinates(string user, int i, int j)
        {
            return Clients.Others.SendAsync("ReceiveCoordinates", user, i, j);
        }

        public Task SendHit(string user, string coordinates, bool wasHit)
        {
            return Clients.All.SendAsync("ReceiveFireInformation", user, coordinates, wasHit);
        }
    }
}
