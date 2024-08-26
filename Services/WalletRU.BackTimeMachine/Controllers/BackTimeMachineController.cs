using Microsoft.AspNetCore.Mvc;
using WalletRU.DAL.Models;
using WalletRU.DAL.Repositories;

namespace WalletRU.BackTimeMachine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BackTimeMachineController : ControllerBase
{
    private readonly ILogger<BackTimeMachineController> _logger;
    private readonly EntityRepository<Message> _repository;

    public BackTimeMachineController(ILogger<BackTimeMachineController> logger, EntityRepository<Message> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("getPostedMessages")]
    public async Task<IActionResult> GetPostedMessages(DateTime period)
    {
        TimeSpan timeSpanPeriod = TimeSpan.Parse(period.ToShortTimeString());
        var messages = _repository.Get(m
            =>  DateTime.UtcNow.Subtract(m.PublishedAt) <= timeSpanPeriod);

        if (!messages.Any())
        {
            _logger.LogError($"! There are no messages in the specified period {period}");
            return NotFound();
        }

        _logger.LogInformation($"[{DateTime.Now}] Returned sent messages in the last {period.TimeOfDay} period");
        return Ok(messages);
    }
}