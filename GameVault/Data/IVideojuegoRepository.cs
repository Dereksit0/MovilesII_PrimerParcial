using GameVault.Models;

namespace GameVault.Data;

/// <summary>
/// Contrato de acceso a datos de la colección.
///
/// En la Fase 1 lo implementa <see cref="VideojuegoRepository"/> sobre una lista
/// hardcodeada en memoria. En la Fase 2 esta misma interfaz se registra en
/// <c>MauiAppBuilder.Services</c> y se inyecta en los ViewModels; en la Fase 4 aparece
/// una segunda implementación (ApiVideojuegoRepository) que consume una API REST con
/// HttpClient. Por eso todos los métodos ya son asíncronos: los ViewModels que se
/// escriben hoy con <c>await</c> no necesitan tocarse cuando cambie la implementación.
/// </summary>
public interface IVideojuegoRepository
{
    /// <summary>Todos los juegos de la colección.</summary>
    Task<IReadOnlyList<Videojuego>> GetVideojuegosAsync();

    /// <summary>Solo los juegos marcados como deseados (wishlist).</summary>
    Task<IReadOnlyList<Videojuego>> GetFavoritosAsync();

    /// <summary>Un juego por Id, o null si no existe.</summary>
    Task<Videojuego?> GetByIdAsync(int id);

    /// <summary>
    /// Inserta el juego si su Id es 0, o actualiza el existente en caso contrario.
    /// Devuelve la instancia ya persistida (con Id asignado si era nueva).
    /// </summary>
    Task<Videojuego> GuardarAsync(Videojuego juego);

    /// <summary>Invierte el estado de favorito de un juego y devuelve el valor resultante.</summary>
    Task<bool> ToggleFavoritoAsync(int id);

    // Catálogos para los Pickers del formulario. Se mantienen síncronos porque son
    // valores fijos del dominio, no datos que viajen por la red.
    IReadOnlyList<string> GetPlataformas();
    IReadOnlyList<string> GetGeneros();
    IReadOnlyList<string> GetEstados();
}
