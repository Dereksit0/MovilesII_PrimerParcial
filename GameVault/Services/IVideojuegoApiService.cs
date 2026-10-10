using GameVault.Models;

namespace GameVault.Services;

public interface IVideojuegoApiService
{
    // GET /odata/Videojuegos?$expand=Resenas
    Task<IReadOnlyList<VideojuegoDto>> ObtenerTodosAsync(CancellationToken cancelacion = default);

    // GET /odata/Videojuegos?$filter=Plataforma eq '...'&$expand=Resenas
    Task<IReadOnlyList<VideojuegoDto>> ObtenerPorPlataformaAsync(
        string plataforma,
        CancellationToken cancelacion = default);

    // GET /odata/Videojuegos(id)?$expand=Resenas
    Task<VideojuegoDto?> ObtenerPorIdAsync(int id, CancellationToken cancelacion = default);

    // GET /odata/Resenas?$filter=VideojuegoId eq id
    Task<IReadOnlyList<ResenaDto>> ObtenerResenasAsync(int videojuegoId, CancellationToken cancelacion = default);

    // POST /odata/Videojuegos
    Task<VideojuegoDto?> CrearAsync(VideojuegoDto nuevo, CancellationToken cancelacion = default);

    // PATCH /odata/Videojuegos(id)
    Task<bool> ActualizarBanderasAsync(
        int id,
        bool esFavorito,
        bool completado,
        CancellationToken cancelacion = default);

    // DELETE /odata/Videojuegos(id)
    Task<bool> EliminarAsync(int id, CancellationToken cancelacion = default);
}
