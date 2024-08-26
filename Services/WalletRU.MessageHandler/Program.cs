using System.Net.WebSockets;
using WalletRU.Core.Services;
using WalletRU.DAL.Models;
using WalletRU.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EntityRepository<Message>, MessageRepository>(provider =>
    new MessageRepository(builder.Configuration.GetConnectionString("postgresConnection")!));

builder.Services.AddTransient<IWebSocketService, WebSocketService>(provider =>
    new WebSocketService(new ClientWebSocket()));

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();