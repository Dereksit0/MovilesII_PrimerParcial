using GameVault.Models;

namespace GameVault.Data;

public class VideojuegoRepository : IVideojuegoRepository
{
    private static readonly List<Videojuego> Juegos = VideojuegoSeedData.Crear();
    private static readonly object Candado = new();
    private static int _siguienteId = Juegos.Max(j => j.Id) + 1;

    private static readonly string[] Plataformas =
    [
        "PC", "PS5", "PS4", "PS2", "PS1", "Xbox Series X", "Xbox 360",
        "Nintendo Switch", "GameCube", "N64", "SNES"
    ];

    private static readonly string[] Generos =
    [
        "Acción", "Aventura", "RPG", "Shooter", "Plataformas",
        "Deportes", "Estrategia", "Terror"
    ];

    private static readonly string[] Estados =
    [
        "En colección", "Deseado", "Completo en caja (CIB)",
        "Solo cartucho/disco", "Vendido"
    ];

    public Task<IReadOnlyList<Videojuego>> GetVideojuegosAsync()
    {
        lock (Candado)
        {
            IReadOnlyList<Videojuego> resultado = Juegos
                .OrderBy(j => j.Titulo, StringComparer.CurrentCultureIgnoreCase)
                .ToList();

            return Task.FromResult(resultado);
        }
    }

    public Task<IReadOnlyList<Videojuego>> GetFavoritosAsync()
    {
        lock (Candado)
        {
            IReadOnlyList<Videojuego> resultado = Juegos
                .Where(j => j.EsFavorito)
                .OrderByDescending(j => j.ValorEstimado)
                .ToList();

            return Task.FromResult(resultado);
        }
    }

    public Task<Videojuego?> GetByIdAsync(int id)
    {
        lock (Candado)
        {
            return Task.FromResult(Juegos.FirstOrDefault(j => j.Id == id));
        }
    }

    public Task<Videojuego> GuardarAsync(Videojuego juego)
    {
        ArgumentNullException.ThrowIfNull(juego);

        lock (Candado)
        {
            if (juego.Id == 0)
            {
                juego.Id = _siguienteId++;
                Juegos.Add(juego);
                return Task.FromResult(juego);
            }

            var existente = Juegos.FirstOrDefault(j => j.Id == juego.Id);
            if (existente is null)
            {
                Juegos.Add(juego);
                return Task.FromResult(juego);
            }

            existente.Titulo = juego.Titulo;
            existente.Plataforma = juego.Plataforma;
            existente.Genero = juego.Genero;
            existente.Estado = juego.Estado;
            existente.ValorEstimado = juego.ValorEstimado;
            existente.ImagenUrl = juego.ImagenUrl;
            existente.ImagenLocalPath = juego.ImagenLocalPath;
            existente.EsFavorito = juego.EsFavorito;
            existente.Completado = juego.Completado;

            return Task.FromResult(existente);
        }
    }

    public Task<bool> ToggleFavoritoAsync(int id)
    {
        lock (Candado)
        {
            var juego = Juegos.FirstOrDefault(j => j.Id == id);
            if (juego is null)
            {
                return Task.FromResult(false);
            }

            juego.EsFavorito = !juego.EsFavorito;
            return Task.FromResult(juego.EsFavorito);
        }
    }

    public IReadOnlyList<string> GetPlataformas() => Plataformas;

    public IReadOnlyList<string> GetGeneros() => Generos;

    public IReadOnlyList<string> GetEstados() => Estados;
}
