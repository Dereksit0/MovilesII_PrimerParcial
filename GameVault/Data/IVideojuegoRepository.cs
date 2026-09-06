using System.Collections.ObjectModel;
using GameVault.Models;

namespace GameVault.Data;

public interface IVideojuegoRepository
{
    ObservableCollection<Videojuego> Videojuegos { get; }

    bool EstaInicializado { get; }

    Task<ResultadoCarga> InicializarAsync(bool forzarRecarga = false, CancellationToken cancelacion = default);

    IReadOnlyList<Videojuego> ObtenerTodos();

    Videojuego? ObtenerPorId(int id);

    Videojuego Agregar(Videojuego juego);

    bool Actualizar(Videojuego juego);

    bool Eliminar(int id);

    IReadOnlyList<string> ObtenerPlataformas();

    IReadOnlyList<string> ObtenerEstados();
}
