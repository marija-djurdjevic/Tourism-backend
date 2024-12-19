using Explorer.Tours.API.Dtos.TourProblemDtos;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

public static class WebSocketHandler
{
    private static readonly ConcurrentDictionary<int, WebSocket> _sockets = new();

    public static async Task HandleAsync(WebSocket webSocket, int userId)
    {
        // Sačuvajte WebSocket vezu sa korisničkim ID-om
        _sockets.TryAdd(userId, webSocket);

        var buffer = new byte[1024 * 4];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            // Obrada primljene poruke (primer)
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Message from user {userId}: {receivedMessage}");

            // Slanje odgovora nazad klijentu
            byte[] responseMessage = Encoding.UTF8.GetBytes($"Hello User {userId}, you said: {receivedMessage}");
            await webSocket.SendAsync(new ArraySegment<byte>(responseMessage), WebSocketMessageType.Text, true, CancellationToken.None);

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        // Zatvaranje veze
        _sockets.TryRemove(userId, out _);
        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        Console.WriteLine($"WebSocket connection closed for user {userId}");
    }
    public static async Task SendMessageToUserAsync(int userId, NotificationDto notification)
    {
        if (_sockets.TryGetValue(userId, out var socket))
        {
            if (socket.State == WebSocketState.Open)
            {
                // Serializacija objekta u JSON
                var jsonMessage = JsonSerializer.Serialize(notification);

                // Pretvaranje JSON-a u bajtove
                var encodedMessage = Encoding.UTF8.GetBytes(jsonMessage);
                var buffer = new ArraySegment<byte>(encodedMessage);

                // Slanje poruke
                await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
