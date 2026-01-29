using TicTacToeServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// CONFIGURAR PUERTO FIJO
// ==========================================
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5251);
});

// ==========================================
// CONFIGURACIÓN CORS
// ==========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins(
            "http://localhost:8081",
            "http://localhost:19006",
            "http://localhost:19000",
            "http://192.168.100.178:8081"  // ? Actualiza con tu IP
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

var app = builder.Build();

// ==========================================
// MIDDLEWARE
// ==========================================
app.UseCors("AllowAll");
app.UseRouting();

// ==========================================
// ENDPOINTS
// ==========================================
app.MapRazorPages();
app.MapHub<GameHub>("/gameHub");

app.MapGet("/api/status", () => new
{
    status = "running",
    game = "TicTacToe - Cliente hace TODO",
    version = "4.0",
    architecture = "Servidor Minimalista (solo retransmite)",
    url = "http://localhost:5251",
    networkUrl = "http://192.168.100.178:5251"  // ? Actualiza con tu IP
});

// ==========================================
// LOGS DE INICIO
// ==========================================
Console.WriteLine("??????????????????????????????????????????");
Console.WriteLine("?   TicTacToe Server (Minimalista)      ?");
Console.WriteLine("??????????????????????????????????????????");
Console.WriteLine();
Console.WriteLine("? Escuchando en:");
Console.WriteLine("   ?? Local:  http://localhost:5251");
Console.WriteLine("   ?? Red:    http://192.168.100.178:5251");
Console.WriteLine("? Hub:   /gameHub");
Console.WriteLine("?? El servidor SOLO retransmite mensajes");
Console.WriteLine("?? El cliente hace TODA la lógica");
Console.WriteLine();

app.Run();