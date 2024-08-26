using System.Net.WebSockets;

namespace WalletRU.Core.Services;

public interface IWebSocketService
{
    public event Action<object>? OnMessageReceived;
    public Task SendAsync<T>(Uri destinationUri, T message);

    public Task HandleMessage<T>(WebSocket socket);
}