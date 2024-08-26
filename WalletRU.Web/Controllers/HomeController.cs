using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WalletRU.DAL.Models;

namespace WalletRU.Web.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _httpClient = new HttpClient();
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(Message message)
    {
        var json = JsonSerializer.Serialize(message);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("http://host.docker.internal:7002/api/Message/postMessage", content);
        _logger.LogInformation(
            $"[{message.PublishedAt}] Transferred message to MessageHandler. Message content: \"{message.MessageBody}\"");

        return View(nameof(Index));
    }
}