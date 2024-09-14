using Microsoft.AspNetCore.SignalR;
namespace BlazorBattleship.Hubs
{
    public class ChatHub : Hub
    {
        //private static int[] roomCounts = { 0, 0, 0, 0, 0 };
        private static List<List<string>> playersInRooms = new List<List<string>>();

        public async Task SendReadyMessage(int room)
        {
            await Clients.OthersInGroup($"Room {room}").SendAsync("ReceiveReadyMessage");
        }

        public async Task UpdateRooms()
        {
            for (int i = 0; i < 5; i++) {
                playersInRooms.Add(new List<string>());
            }
            await Clients.All.SendAsync("ReceiveRooms", playersInRooms);
        }

        public async Task JoinRoom(string user, int room)
        {
            while (playersInRooms.Count != 5)
            {
                playersInRooms.Add(new List<string>());
            }

            if (playersInRooms[room-1].Count < 2)
            {
                playersInRooms[room-1].Add(user);
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, $"Room {room}");
            await Clients.All.SendAsync("ReceiveRooms", playersInRooms);
        }

        public async Task SendCoordinates(string user, int room, int x, int y)
        {
            await Clients.OthersInGroup($"Room {room}").SendAsync("ReceiveCoordinates", user, x, y);
        }

        public async Task SendHitMiss(int room, bool wasHit)
        {
            await Clients.OthersInGroup($"Room {room}").SendAsync("ShotResponse", wasHit);
        }
    }
}
