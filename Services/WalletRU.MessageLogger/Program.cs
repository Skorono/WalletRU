using System.Net.WebSockets;
using WalletRU.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IWebSocketService, WebSocketService>(provider => 
    new WebSocketService(new ClientWebSocket()));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.UseWebSockets();

app.UseHttpsRedirection();

app.Run();
