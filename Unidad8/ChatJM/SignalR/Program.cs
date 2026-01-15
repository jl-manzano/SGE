using SignalRChat.Hubs;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURACIÓN DE SERVICIOS ---
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

// AÑADIR ESTO: Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactNative", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .SetIsOriginAllowed((host) => true) // Permite cualquier origen (ideal para apps móviles)
              .AllowCredentials();               // Obligatorio para SignalR
    });
});

var app = builder.Build();

// --- 2. CONFIGURACIÓN DEL PIPELINE (MIDDLEWARE) ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// AÑADIR ESTO: Activar CORS antes de Authorization y MapHub
app.UseCors("AllowReactNative");

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");

app.Run();