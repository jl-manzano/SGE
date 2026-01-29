using TicTacToe.CompositionRoot;
using TicTacToe.Presentation.Hubs;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// DETECCIÓN AUTOMÁTICA DE IPS
// ==========================================
string GetLocalIPAddress()
{
    try
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                string ipString = ip.ToString();
                // Ignorar IPs de VirtualBox y localhost
                if (!ipString.StartsWith("192.168.56.") &&
                    !ipString.StartsWith("127.") &&
                    !ipString.StartsWith("169.254."))
                {
                    return ipString;
                }
            }
        }
    }
    catch { }
    return "127.0.0.1";
}

string localIp = GetLocalIPAddress();

// ==========================================
// CONFIGURAR PUERTO FIJO EN TODAS LAS INTERFACES
// ==========================================
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // ✅ CRÍTICO: ListenAnyIP escucha en TODAS las interfaces (0.0.0.0)
    // Esto permite conexiones desde localhost Y desde tu IP de red
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
            "http://127.0.0.1:8081",
            $"http://{localIp}:8081",
            // Permitir cualquier origen desde la red local (más flexible para desarrollo)
            "http://192.168.0.0:8081",  // Placeholder para subnet
            "http://172.20.10.0:8081"    // Placeholder para subnet
        )
        .SetIsOriginAllowed(origin =>
        {
            // Durante desarrollo, permitir todas las conexiones locales
            if (string.IsNullOrEmpty(origin)) return false;

            var uri = new Uri(origin);
            var host = uri.Host;

            // Permitir localhost en todas sus formas
            if (host == "localhost" || host == "127.0.0.1") return true;

            // Permitir IPs de red local
            if (host.StartsWith("192.168.") ||
                host.StartsWith("172.") ||
                host.StartsWith("10.")) return true;

            return false;
        })
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
    game = "TicTacToe - Sistema de Salas",
    version = "3.2",
    architecture = "Domain + Presentation + CompositionRoot",
    localhost = "http://localhost:5251",
    networkUrl = $"http://{localIp}:5251",
    detectedIP = localIp
});

// ==========================================
// LOGS DE INICIO CON IP DETECTADA
// ==========================================
Console.WriteLine("╔════════════════════════════════════════════════════════╗");
Console.WriteLine("║   TicTacToe Server - Sistema de Salas                ║");
Console.WriteLine("╚════════════════════════════════════════════════════════╝");
Console.WriteLine();
Console.WriteLine("✅ Servidor iniciado correctamente");
Console.WriteLine();
Console.WriteLine("📡 Escuchando en TODAS las interfaces (0.0.0.0:5251):");
Console.WriteLine();
Console.WriteLine("   🏠 Localhost:");
Console.WriteLine("      http://localhost:5251");
Console.WriteLine("      http://127.0.0.1:5251");
Console.WriteLine();
Console.WriteLine($"   🌐 Red Local (IP detectada: {localIp}):");
Console.WriteLine($"      http://{localIp}:5251");
Console.WriteLine();
Console.WriteLine("   🎮 SignalR Hub:");
Console.WriteLine("      /gameHub");
Console.WriteLine();
Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
Console.WriteLine();
Console.WriteLine("📱 CONFIGURACIÓN PARA REACT NATIVE:");
Console.WriteLine();
Console.WriteLine("   1️⃣  Si desarrollas en la MISMA PC:");
Console.WriteLine("       const HUB_URL = \"http://localhost:5251/gameHub\";");
Console.WriteLine();
Console.WriteLine("   2️⃣  Si usas Android Emulator:");
Console.WriteLine("       const HUB_URL = \"http://10.0.2.2:5251/gameHub\";");
Console.WriteLine();
Console.WriteLine($"   3️⃣  Si usas dispositivo físico en red WiFi:");
Console.WriteLine($"       const HUB_URL = \"http://{localIp}:5251/gameHub\";");
Console.WriteLine();
Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
Console.WriteLine();
Console.WriteLine("🧪 Prueba la conexión:");
Console.WriteLine($"   curl http://{localIp}:5251/api/status");
Console.WriteLine($"   O abre en navegador: http://{localIp}:5251");
Console.WriteLine();
Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
Console.WriteLine();
Console.WriteLine("⚠️  IMPORTANTE:");
Console.WriteLine("   - Asegúrate de que el Firewall permita el puerto 5251");
Console.WriteLine("   - Frontend y backend deben usar la MISMA IP");
Console.WriteLine("   - Para red local, PC y dispositivo en misma WiFi");
Console.WriteLine();
Console.WriteLine("🔥 Presiona Ctrl+C para detener el servidor");
Console.WriteLine();

app.Run();