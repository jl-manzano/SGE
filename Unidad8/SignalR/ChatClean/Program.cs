using ChatClean.Hubs;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();

// ✅ Configurar SignalR con opciones JSON explícitas
builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        // Usar camelCase para serialización (usuario, mensaje en minúscula)
        options.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.PayloadSerializerOptions.PropertyNameCaseInsensitive = true;

        // Configuración adicional para debugging
        options.PayloadSerializerOptions.WriteIndented = true;
    });

// ✅ Configurar CORS para React Native
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)  // Permitir cualquier origen
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// ✅ Usar CORS ANTES de UseRouting
app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");

Console.WriteLine("🚀 Servidor iniciado");
Console.WriteLine($"📡 Hub disponible en: /chatHub");

app.Run();