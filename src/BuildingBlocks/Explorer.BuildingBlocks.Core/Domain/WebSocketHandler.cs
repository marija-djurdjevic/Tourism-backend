using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.Domain
{

    public static class WebSocketHandler
    {
        private static readonly ConcurrentDictionary<int, WebSocket> _sockets = new();

        public static async Task HandleAsync(WebSocket socket)
        {
            // Primer ID korisnika (u stvarnosti, povežite ovo sa autentifikacijom)
            int userId = GetUserIdFromContext();

            _sockets.TryAdd(userId, socket);

            var buffer = new byte[1024 * 4];
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                // Logika za primanje poruka (ako je potrebno)
                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            // Kada korisnik prekine vezu
            _sockets.TryRemove(userId, out _);
            await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        public static async Task SendMessageToUserAsync(int userId, string message)
        {
            if (_sockets.TryGetValue(userId, out var socket))
            {
                if (socket.State == WebSocketState.Open)
                {
                    var encodedMessage = Encoding.UTF8.GetBytes(message);
                    var buffer = new ArraySegment<byte>(encodedMessage);
                    await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }

        private static int GetUserIdFromContext()
        {
            // Prilagodite logiku za dobijanje korisničkog ID-a
            return 123; // Primer korisničkog ID-a
        }
    }

}
