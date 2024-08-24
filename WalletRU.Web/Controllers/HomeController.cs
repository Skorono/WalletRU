using Microsoft.AspNetCore.Mvc;
using WalletRU.DAL.Repositories;

namespace WalletRU.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var repository = new MessageRepository("Host=localhost;Port=5432;Database=WalletRuDB;Username=postgres;Password=postgres");
        TempData["Data"] = repository.Get();
        return View();
    }
}