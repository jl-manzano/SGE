using TicTacToe.Hubs;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// CONFIGURACIÓN CORS - CRÍTICO
// ==========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowExpo", policy =>
    {
        policy.WithOrigins(
            "http://localhost:8081",      // Expo Web
            "http://localhost:19006",     // Expo Web alternativo
            "http://localhost:19000",     // Expo Dev Tools
            "http://192.168.1.100:8081"   // ? CAMBIA por tu IP si usas móvil
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
builder.Services.AddRazorPages(); // Para Index.cshtml

var app = builder.Build();

// ==========================================
// MIDDLEWARE - ¡ORDEN IMPORTANTE!
// ==========================================

// ? COMENTAR O ELIMINAR ESTA LÍNEA:
// app.UseHttpsRedirection(); // ? Causa problemas con CORS

// ? CORS DEBE IR PRIMERO
app.UseCors("AllowExpo");

app.UseRouting();

// ==========================================
// ENDPOINTS
// ==========================================

// Página de inicio
app.MapRazorPages();

// SignalR Hub
app.MapHub<GameHub>("/gameHub");

// API de estado
app.MapGet("/api/status", () => new
{
    status = "running",
    game = "TicTacToe",
    version = "1.0",
    cors = "enabled",
    httpsRedirect = "disabled",
    message = "SignalR server running without HTTPS redirect"
});

// ==========================================
// LOGS DE INICIO
// ==========================================
Console.WriteLine("??????????????????????????????????????????");
Console.WriteLine("?   TicTacToe SignalR Server Started    ?");
Console.WriteLine("??????????????????????????????????????????");
Console.WriteLine();
Console.WriteLine("? HTTP:  http://localhost:5251");
Console.WriteLine("? HTTPS: https://localhost:7238");
Console.WriteLine("? Hub:   /gameHub");
Console.WriteLine("? CORS:  Habilitado para Expo");
Console.WriteLine("??  HTTPS Redirect: DESHABILITADO");
Console.WriteLine();
Console.WriteLine("?? Conecta desde tu app:");
Console.WriteLine("   const HUB_URL = 'http://localhost:5251/gameHub';");
Console.WriteLine();

app.Run();