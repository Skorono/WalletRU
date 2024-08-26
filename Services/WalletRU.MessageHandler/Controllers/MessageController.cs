using Microsoft.AspNetCore.Mvc;
using WalletRU.Core.Services;
using WalletRU.DAL.Models;
using WalletRU.DAL.Repositories;

namespace WalletRU.MessageHandler.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController: ControllerBase
{
    private EntityRepository<Message> _repository;
    private readonly IWebSocketService _webSocketService;
    private readonly ILogger<MessageController> _logger;
    
    public MessageController(ILogger<MessageController> logger, EntityRepository<Message> repository, IWebSocketService webSocketService)
    {
        _repository = repository;
        _webSocketService = webSocketService;
        _logger = logger;
    }

    [HttpPost("postMessage")]
    public async Task PostMessage(Message message)
    {
        await new TaskFactory().StartNew(() => _repository.Add(message));
        await _webSocketService.SendAsync(new Uri("ws://localhost:7003/api/Logger/logMessage"), message);
        
        _logger.LogInformation($"[{DateTime.Now}] Added message to database #{message.Id}");
    }
}