using Microsoft.AspNetCore.SignalR;
namespace BlazorBattleship.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendCoordinates(string user, int x, int y)
        {
            await Clients.Others.SendAsync("ReceiveCoordinates", user, x, y);
        }

        public async Task SendHit(string user, string coordinates, bool wasHit)
        {
            await Clients.All.SendAsync("ReceiveFireInformation", user, coordinates, wasHit);
        }
    }
}
