using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using ConexaoSolidaria.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =============================================================================
// SERVIÇOS (Dependency Injection)
// =============================================================================

// Conexão com o Azure SQL Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' nao foi encontrada. " +
        "Configure-a no appsettings.json (desenvolvimento) ou nas Application Settings " +
        "do Azure App Service (produção).");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// ASP.NET Core Identity (atende RF01, RF02 e RF13)
builder.Services
    .AddDefaultIdentity<Usuario>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<AppDbContext>();

// Cookie de autenticacao - rota das paginas customizadas
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/Login";
    options.LogoutPath = "/Logout";
});

// Servico de upload para Azure Blob Storage (fotos de perfil e anexos)
builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();

// Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// =============================================================================
// PIPELINE HTTP
// =============================================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

// =============================================================================
// SEED DO BANCO (apenas em desenvolvimento ou na primeira execucao)
// =============================================================================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
    await DbSeeder.SeedAsync(db, userManager);
}

app.Run();
