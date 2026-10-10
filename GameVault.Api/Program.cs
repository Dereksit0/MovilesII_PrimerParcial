using GameVault.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var cadena = builder.Configuration.GetConnectionString("GameVault")
             ?? "Data Source=gamevault.db";

builder.Services.AddDbContext<GameVaultContext>(opciones => opciones.UseSqlite(cadena));

var app = builder.Build();

// La base se vuelve a crear en cada arranque para que la demo siempre salga del mismo estado.
using (var alcance = app.Services.CreateScope())
{
    var contexto = alcance.ServiceProvider.GetRequiredService<GameVaultContext>();
    contexto.Database.EnsureDeleted();
    contexto.Database.EnsureCreated();
}

app.MapControllers();

app.MapGet("/", () => "GameVault.Api lista. Los datos salen de /odata.");

app.Run();
