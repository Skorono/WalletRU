using Microsoft.AspNetCore.Mvc;
using WalletRU.DAL.Models;
using WalletRU.DAL.Repositories;

namespace WalletRU.BackTimeMachine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BackTimeMachineController: ControllerBase
{
    private readonly ILogger<BackTimeMachineController> _logger;
    private readonly EntityRepository<Message> _repository;
    
    public BackTimeMachineController(ILogger<BackTimeMachineController> logger, EntityRepository<Message> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("getPostedMessages")]
    public async Task<IActionResult> GetPostedMessages(TimeOnly period)
    {
        return Ok(_repository.Get(m 
            => (DateTime.Now - m.PublishedAt) <= period.ToTimeSpan()));
    }
}