using GameVault.Models;

namespace GameVault.Data;

public interface IVideojuegoRepository
{
    Task<IReadOnlyList<Videojuego>> GetVideojuegosAsync();

    Task<IReadOnlyList<Videojuego>> GetFavoritosAsync();

    Task<Videojuego?> GetByIdAsync(int id);

    Task<Videojuego> GuardarAsync(Videojuego juego);

    Task<bool> ToggleFavoritoAsync(int id);

    IReadOnlyList<string> GetPlataformas();
    IReadOnlyList<string> GetGeneros();
    IReadOnlyList<string> GetEstados();
}
