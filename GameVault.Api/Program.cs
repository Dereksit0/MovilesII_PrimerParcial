using GameVault.Api.Data;
using GameVault.Api.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// El cliente MAUI Blazor Hybrid corre dentro de un WebView con su propio origen,
// asi que dejamos pasar cualquier origen mientras trabajamos en local.
const string PoliticaLocal = "gamevault-local";

builder.Services.AddCors(opciones => opciones.AddPolicy(
    PoliticaLocal,
    politica => politica
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));

builder.Services.AddControllers()
    .AddOData(opciones => opciones
        .Select()
        .Filter()
        .OrderBy()
        .Expand()
        .Count()
        .SetMaxTop(200)
        .AddRouteComponents("odata", ConstruirModelo()));

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
    SembradorDeDatos.Sembrar(contexto);
}

app.UseCors(PoliticaLocal);

app.MapControllers();

app.MapGet("/", () => Results.Text(
    """
    GameVault.Api lista.

    /odata/$metadata
    /odata/Videojuegos
    /odata/Videojuegos?$expand=Resenas
    /odata/Videojuegos?$filter=Plataforma eq 'PC'
    /odata/Videojuegos(1)?$expand=Resenas
    /odata/Resenas?$filter=VideojuegoId eq 1
    """,
    "text/plain"));

app.Run();

static IEdmModel ConstruirModelo()
{
    var constructor = new ODataConventionModelBuilder();

    constructor.EntitySet<Videojuego>("Videojuegos");
    constructor.EntitySet<Resena>("Resenas");

    return constructor.GetEdmModel();
}
