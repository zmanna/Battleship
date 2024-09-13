using Microsoft.AspNetCore.SignalR;
namespace BlazorBattleship.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendCoordinates(string user, int x, int y)
        {
            await Clients.Others.SendAsync("ReceiveCoordinates", user, x, y);
        }

        public async Task SendHitMiss(bool wasHit)
        {
            await Clients.Others.SendAsync("ShotResponse", wasHit);
        }

    }
}
