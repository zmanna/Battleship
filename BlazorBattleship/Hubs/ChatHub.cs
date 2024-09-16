﻿using BlazorBattleship.Shared;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
namespace BlazorBattleship.Hubs
{
    public class ChatHub : Hub
    {
        //private static int[] roomCounts = { 0, 0, 0, 0, 0 };
        private static List<List<string>> playersInRooms = new List<List<string>>();

        public async Task SendShipNum(int room, int shipLimit)
        {
            await Clients.Group($"Room {room}").SendAsync("ReceiveShipNum", shipLimit);
        }

        public async Task SendReadyMessage(int room, int shipLimit)
        {
            await Clients.OthersInGroup($"Room {room}").SendAsync("ReceiveReadyMessage");
        }

        /*
        public async Task UpdateRooms()
        {
            for (int i = 0; i < 5; i++) {
                playersInRooms.Add(new List<string>());
            }
            await Clients.All.SendAsync("ReceiveRooms", playersInRooms);
        }
        */

        public async Task GetRooms()
        {
            while (playersInRooms.Count != 5)
            {
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
            if (playersInRooms[room - 1].Count < 2 && playersInRooms[room - 1].Contains(user) == false)
            {
                playersInRooms[room - 1].Add(user);
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, $"Room {room}");
            await Clients.All.SendAsync("ReceiveRooms", playersInRooms);
        }

        public async Task LeaveRoom(string user, int room)
        {
            playersInRooms[room - 1].Remove(user);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Room {room}");
            await Clients.All.SendAsync("ReceiveRooms", playersInRooms);
            await Clients.Group($"Room {room}").SendAsync("PlayerLeft", user);
        }

        public async Task SendCoordinates(string user, int room, int x, int y)
        {
            await Clients.OthersInGroup($"Room {room}").SendAsync("ReceiveCoordinates", user, x, y);
        }

        public async Task SendHitMiss(int room, bool wasHit)//, bool wasSunk)
        {
            await Clients.OthersInGroup($"Room {room}").SendAsync("ShotResponse", wasHit);//, wasSunk);
        }

        public async Task SendMessage(string userName, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", userName,  message);
        }
    }
}
