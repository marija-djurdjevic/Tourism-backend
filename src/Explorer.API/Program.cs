using Explorer.API.Startup;
using System.Net.WebSockets;
using Explorer.BuildingBlocks.Core.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureSwagger(builder.Configuration);
const string corsPolicy = "_corsPolicy";
builder.Services.ConfigureCors(corsPolicy);
builder.Services.ConfigureAuth();

builder.Services.RegisterModules();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseRouting();
app.UseCors(corsPolicy);
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.UseWebSockets();

app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        // Izvla?enje userId iz query string-a
        var userIdQuery = context.Request.Query["userId"];

        if (string.IsNullOrEmpty(userIdQuery) || !int.TryParse(userIdQuery, out var userId))
        {
            // Ako userId nije validan, vratite grešku
            context.Response.StatusCode = 400; // Bad Request
            await context.Response.WriteAsync("Invalid or missing userId");
            return;
        }

        // Prihvatanje WebSocket veze
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();

        // Prosle?ivanje userId WebSocket handleru
        await WebSocketHandler.HandleAsync(webSocket, userId);
    }
    else
    {
        context.Response.StatusCode = 400; // Bad Request
    }
});


app.Run();

// Required for automated tests
namespace Explorer.API
{
    public partial class Program { }
}