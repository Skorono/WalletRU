using Microsoft.AspNetCore.Mvc;
using WalletRU.DAL.Models;
using WalletRU.DAL.Repositories;

namespace WalletRU.MessageHandler.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController: ControllerBase
{
    private EntityRepository<Message> _repository;
    private readonly ILogger<MessageController> _logger;
    
    public MessageController(ILogger<MessageController> logger, EntityRepository<Message> repository)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpPost("postMessage")]
    public void PostMessage(Message message)
    {
        _repository.Add(message);
    }
}