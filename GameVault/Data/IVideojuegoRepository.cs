using GameVault.Models;

namespace GameVault.Data;

public interface IVideojuegoRepository
{
    IReadOnlyList<VideojuegoDto> Videojuegos { get; }

    bool EstaInicializado { get; }

    // Los componentes Razor se suscriben para volver a dibujarse cuando cambia la coleccion.
    event Action? CambioLaColeccion;

    Task<ResultadoCarga> InicializarAsync(bool forzarRecarga = false, CancellationToken cancelacion = default);

    VideojuegoDto? ObtenerPorId(int id);

    Task<VideojuegoDto?> ObtenerConResenasAsync(int id, CancellationToken cancelacion = default);

    IReadOnlyList<VideojuegoDto> ObtenerWishlist();

    IReadOnlyList<VideojuegoDto> ObtenerTerminados();

    Task<ResultadoCarga> AgregarAsync(VideojuegoDto nuevo, CancellationToken cancelacion = default);

    Task<ResultadoCarga> AlternarFavoritoAsync(int id, CancellationToken cancelacion = default);

    Task<ResultadoCarga> AlternarCompletadoAsync(int id, CancellationToken cancelacion = default);

    Task<ResultadoCarga> EliminarAsync(int id, CancellationToken cancelacion = default);

    IReadOnlyList<string> ObtenerPlataformas();

    IReadOnlyList<string> ObtenerEstados();
}
