using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.Json;
using GameVault.Models;

namespace GameVault.Data;

public class VideojuegoRepository : IVideojuegoRepository
{
    private const string PlantillaRuta = "api/1.0/deals?storeID=1&pageSize=60&pageNumber={0}&sortBy=Metacritic&steamRating=80";
    private const int PaginasACargar = 4;
    private const string PortadaSteam = "https://cdn.cloudflare.steamstatic.com/steam/apps/{0}/library_600x900.jpg";
    private const decimal TipoDeCambio = 18.50m;

    private static readonly string[] Plataformas =
    [
        "PC", "PS5", "PS4", "PS2", "PS1", "Xbox Series X", "Xbox 360",
        "Nintendo Switch", "GameCube", "N64", "SNES"
    ];

    private static readonly string[] Generos =
    [
        "Sin clasificar", "Acción", "Aventura", "RPG", "Shooter",
        "Plataformas", "Deportes", "Estrategia", "Terror"
    ];

    private static readonly string[] Estados =
    [
        "En colección", "Deseado", "Completo en caja (CIB)",
        "Solo cartucho/disco", "Vendido"
    ];

    private static readonly JsonSerializerOptions OpcionesJson = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _http;
    private int _siguienteId = 1;

    public VideojuegoRepository(HttpClient http)
    {
        _http = http;
    }

    public ObservableCollection<Videojuego> Videojuegos { get; } = [];

    public bool EstaInicializado { get; private set; }

    public async Task<ResultadoCarga> InicializarAsync(bool forzarRecarga = false, CancellationToken cancelacion = default)
    {
        if (EstaInicializado && !forzarRecarga)
        {
            return ResultadoCarga.Ok();
        }

        try
        {
            var ofertas = new List<OfertaJuegoDto>();

            for (var pagina = 0; pagina < PaginasACargar; pagina++)
            {
                var ruta = string.Format(CultureInfo.InvariantCulture, PlantillaRuta, pagina);

                using var respuesta = await _http.GetAsync(ruta, cancelacion);
                respuesta.EnsureSuccessStatusCode();

                await using var flujo = await respuesta.Content.ReadAsStreamAsync(cancelacion);
                var ofertasDeLaPagina = await JsonSerializer.DeserializeAsync<List<OfertaJuegoDto>>(
                    flujo, OpcionesJson, cancelacion);

                if (ofertasDeLaPagina is null || ofertasDeLaPagina.Count == 0)
                {
                    break;
                }

                ofertas.AddRange(ofertasDeLaPagina);
            }

            Videojuegos.Clear();
            _siguienteId = 1;

            var vistos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var oferta in ofertas)
            {
                var clave = string.IsNullOrWhiteSpace(oferta.SteamAppId)
                    ? oferta.Title ?? string.Empty
                    : oferta.SteamAppId;

                if (!vistos.Add(clave))
                {
                    continue;
                }

                var juego = Mapear(oferta);
                if (juego is not null)
                {
                    Videojuegos.Add(juego);
                }
            }

            EstaInicializado = true;
            return ResultadoCarga.Ok();
        }
        catch (TaskCanceledException)
        {
            return ResultadoCarga.Fallo(
                "La solicitud tardó demasiado y se canceló. Revisa tu conexión e inténtalo de nuevo.");
        }
        catch (HttpRequestException ex)
        {
            return ResultadoCarga.Fallo(
                $"No se pudo contactar el servicio de juegos. {ex.Message}");
        }
        catch (JsonException)
        {
            return ResultadoCarga.Fallo(
                "El servicio respondió con un formato que la app no pudo interpretar.");
        }
    }

    public IReadOnlyList<Videojuego> ObtenerTodos() => Videojuegos;

    public Videojuego? ObtenerPorId(int id) => Videojuegos.FirstOrDefault(j => j.Id == id);

    public Videojuego Agregar(Videojuego juego)
    {
        ArgumentNullException.ThrowIfNull(juego);

        juego.Id = _siguienteId++;
        Videojuegos.Add(juego);
        return juego;
    }

    public bool Actualizar(Videojuego juego)
    {
        ArgumentNullException.ThrowIfNull(juego);

        var indice = IndiceDe(juego.Id);
        if (indice < 0)
        {
            return false;
        }

        Videojuegos[indice] = juego;
        return true;
    }

    public bool Eliminar(int id)
    {
        var indice = IndiceDe(id);
        if (indice < 0)
        {
            return false;
        }

        Videojuegos.RemoveAt(indice);
        return true;
    }

    public IReadOnlyList<string> ObtenerPlataformas() => Plataformas;

    public IReadOnlyList<string> ObtenerGeneros() => Generos;

    public IReadOnlyList<string> ObtenerEstados() => Estados;

    private int IndiceDe(int id)
    {
        for (var i = 0; i < Videojuegos.Count; i++)
        {
            if (Videojuegos[i].Id == id)
            {
                return i;
            }
        }

        return -1;
    }

    private Videojuego? Mapear(OfertaJuegoDto oferta)
    {
        if (string.IsNullOrWhiteSpace(oferta.Title))
        {
            return null;
        }

        var enOferta = oferta.IsOnSale == "1";

        return new Videojuego
        {
            Id = _siguienteId++,
            Titulo = oferta.Title.Trim(),
            Plataforma = "PC",
            Genero = "Sin clasificar",
            Estado = enOferta ? "Deseado" : "En colección",
            ValorEstimado = Math.Round(ParsearPrecio(oferta.NormalPrice) * TipoDeCambio, 2),
            ImagenUrl = ConstruirPortada(oferta.SteamAppId),
            EsFavorito = enOferta,
            Completado = false
        };
    }

    private static decimal ParsearPrecio(string? valor) =>
        decimal.TryParse(valor, NumberStyles.Number, CultureInfo.InvariantCulture, out var precio)
            ? precio
            : 0m;

    private static string ConstruirPortada(string? steamAppId) =>
        string.IsNullOrWhiteSpace(steamAppId) || !steamAppId.All(char.IsDigit)
            ? string.Empty
            : string.Format(CultureInfo.InvariantCulture, PortadaSteam, steamAppId);
}
