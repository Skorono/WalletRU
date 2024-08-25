﻿using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WalletRU.DAL.Models;
using WalletRU.DAL.Repositories;

namespace WalletRU.Web.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _httpClient = new();
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
        await _httpClient.PostAsync("http://localhost:5272/api/Message/postMessage", content);

        return View(nameof(Index));
    }
}