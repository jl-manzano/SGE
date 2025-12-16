using CompositionRoot;

var builder = WebApplication.CreateBuilder(args);

// Agregar CompositionRoot
builder.Services.AddCompositionRoot(builder.Configuration);

// Registrar servicios de MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el pipeline de middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
