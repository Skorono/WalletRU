using Microsoft.AspNetCore.Mvc;
using WalletRU.Core.Services;
using WalletRU.DAL.Models;

namespace WalletRU.MessageLogger.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoggerController: ControllerBase
{
    private readonly IWebSocketService _socketService;
    private readonly ILogger<LoggerController> _logger;

    public LoggerController(ILogger<LoggerController> logger, IWebSocketService socketService)
    {
        _logger = logger;
        _socketService = socketService;
        _socketService.OnMessageReceived += message =>
            _logger.LogInformation(
                $"[{DateTime.Now}] " +
                $"\n\tId: {((Message)message).Id} " +
                $"\n\tBody: \"{((Message)message).MessageBody}\" " +
                $"\n\tPublished At {((Message)message).PublishedAt}");
    }
    
    [Route("logMessage")]
    public async Task LogMessage()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await _socketService.HandleMessage<Message>(webSocket);
        }
    }
}