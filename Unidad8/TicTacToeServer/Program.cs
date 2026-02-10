using TicTacToeServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// CONFIGURAR PUERTO (Kestrel solo para local, Azure ignora esto)
// ==========================================
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5251, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
    });
});

// ==========================================
// CONFIGURACIÓN CORS
// ==========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ==========================================
// SERVICIOS
// ==========================================
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.KeepAliveInterval = TimeSpan.FromSeconds(10);
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
});

builder.Services.AddRazorPages();

var app = builder.Build();

// ==========================================
// MIDDLEWARE
// ==========================================
app.UseCors("AllowAll");
app.UseWebSockets();
app.UseRouting();

// ==========================================
// ENDPOINTS
// ==========================================
app.MapRazorPages();
app.MapHub<GameHub>("/gameHub");

app.MapGet("/api/status", () => new
{
    status = "running",
    game = "TicTacToe",
    version = "5.0",
    environment = builder.Environment.EnvironmentName,
    isAzure = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"))
});

// ==========================================
// LOG DE INICIO (mínimo)
// ==========================================
var isAzure = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"));
var url = isAzure
    ? "https://tictactoeserver-dyb5dggmhyhfa2gh.spaincentral-01.azurewebsites.net"
    : "http://localhost:5251";

Console.WriteLine($"TicTacToe Server iniciado en {url}/gameHub");

app.Run();