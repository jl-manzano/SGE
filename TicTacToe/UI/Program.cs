using TicTacToe.CompositionRoot;
using TicTacToe.Presentation.Hubs;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// CONFIGURAR PUERTO FIJO EN TODAS LAS INTERFACES
// ==========================================
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // ✅ CRÍTICO: ListenAnyIP escucha en TODAS las interfaces (0.0.0.0)
    // Esto permite conexiones desde localhost Y desde tu IP de red (192.168.100.178)
    serverOptions.ListenAnyIP(5251);
});

// ==========================================
// CONFIGURACIÓN CORS
// ==========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowExpo", policy =>
    {
        policy.WithOrigins(
            "http://localhost:8081",
            "http://localhost:19006",
            "http://localhost:19000",
            "http://192.168.100.178:8081"  // ✅ IP ACTUALIZADA
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// ==========================================
// SERVICIOS
// ==========================================
builder.Services.AddSignalR();
builder.Services.AddRazorPages();
builder.Services.AddTicTacToeServices();

var app = builder.Build();

// ==========================================
// MIDDLEWARE
// ==========================================
app.UseCors("AllowExpo");
app.UseRouting();

// ==========================================
// ENDPOINTS
// ==========================================
app.MapRazorPages();
app.MapHub<GameHub>("/gameHub");

app.MapGet("/api/status", () => new
{
    status = "running",
    game = "TicTacToe - UNA SOLA PARTIDA",
    version = "3.1",
    architecture = "Domain + Presentation + CompositionRoot",
    url = "http://localhost:5251",
    networkUrl = "http://192.168.100.178:5251"  // ✅ IP ACTUALIZADA
});

// ==========================================
// LOGS DE INICIO
// ==========================================
Console.WriteLine("╔════════════════════════════════════════╗");
Console.WriteLine("║   TicTacToe Server                    ║");
Console.WriteLine("╚════════════════════════════════════════╝");
Console.WriteLine();
Console.WriteLine("✅ Escuchando en TODAS las interfaces:");
Console.WriteLine("   📍 Localhost:  http://localhost:5251");
Console.WriteLine("   📍 Red Local:  http://192.168.100.178:5251");  // ✅ IP ACTUALIZADA
Console.WriteLine("✅ Hub:   /gameHub");
Console.WriteLine("📦 El servidor administra UNA SOLA partida");
Console.WriteLine();
Console.WriteLine("📡 Para conectar desde React Native:");
Console.WriteLine("   Actualiza App.tsx con:");
Console.WriteLine("   const HUB_URL = \"http://192.168.100.178:5251/gameHub\";");  // ✅ IP ACTUALIZADA
Console.WriteLine();

app.Run();