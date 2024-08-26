using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace WalletRU.Core.Services;

public class WebSocketService: IWebSocketService
{
    private ClientWebSocket _socket;
    public event Action<object>? OnMessageReceived;

    public WebSocketService(ClientWebSocket socket)
    {
        _socket = socket;
    }

    public async Task SendAsync<T>(Uri destinationUri, T message)
    {
        await _socket.ConnectAsync(destinationUri, CancellationToken.None);
        
        byte[] messageContent = JsonSerializer.SerializeToUtf8Bytes(message);
        await _socket.SendAsync(new ArraySegment<byte>(messageContent),
            WebSocketMessageType.Text, true, CancellationToken.None);

        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, 
            "Client closed", CancellationToken.None);
    }

    public async Task HandleMessage<T>(WebSocket socket)
    {
        T? message = default(T);
        var buffer = new byte[1024 * 4];
        var receiveResult = await socket.ReceiveAsync(new ArraySegment<byte>(buffer),
            CancellationToken.None);

        while (!receiveResult.CloseStatus.HasValue)
        {
            if (receiveResult.EndOfMessage)
            {
                message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(buffer, 0, receiveResult.Count))!;
                receiveResult = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            else
            {
                using (MemoryStream stream = new())
                {
                    do
                    {
                        stream.Write(buffer, 0, receiveResult.Count);
                        receiveResult = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    } while (!receiveResult.EndOfMessage);

                    stream.Seek(0, SeekOrigin.Begin);
                    message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(buffer, 0, receiveResult.Count))!;
                }
            }
        }

        OnMessageReceived?.Invoke(message!);

        await socket.CloseAsync(receiveResult.CloseStatus.Value, 
            receiveResult.CloseStatusDescription, CancellationToken.None);
    }
}
