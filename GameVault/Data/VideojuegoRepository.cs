using System.Text.Json;
using GameVault.Models;
using GameVault.Services;

namespace GameVault.Data;

// Ya no habla con CheapShark: ahora todo sale de GameVault.Api por OData.
// Guarda en memoria lo ultimo que trajo el servicio para que la UI no vuelva a pedir en cada clic.
public class VideojuegoRepository : IVideojuegoRepository
{
    private readonly IVideojuegoApiService _api;
    private readonly List<VideojuegoDto> _juegos = [];

    public VideojuegoRepository(IVideojuegoApiService api)
    {
        _api = api;
    }

    public IReadOnlyList<VideojuegoDto> Videojuegos => _juegos;

    public bool EstaInicializado { get; private set; }

    public event Action? CambioLaColeccion;

    public async Task<ResultadoCarga> InicializarAsync(
        bool forzarRecarga = false,
        CancellationToken cancelacion = default)
    {
        if (EstaInicializado && !forzarRecarga)
        {
            return ResultadoCarga.Ok();
        }

        return await EjecutarAsync(async () =>
        {
            var juegos = await _api.ObtenerTodosAsync(cancelacion);

            _juegos.Clear();
            _juegos.AddRange(juegos);

            EstaInicializado = true;
            Avisar();
        });
    }

    public VideojuegoDto? ObtenerPorId(int id) => _juegos.FirstOrDefault(juego => juego.Id == id);

    public async Task<VideojuegoDto?> ObtenerConResenasAsync(int id, CancellationToken cancelacion = default)
    {
        var juego = await _api.ObtenerPorIdAsync(id, cancelacion);

        if (juego is null)
        {
            return null;
        }

        var indice = _juegos.FindIndex(actual => actual.Id == id);

        if (indice >= 0)
        {
            _juegos[indice] = juego;
        }

        return juego;
    }

    // Reglas que ya traiamos del primer parcial: la wishlist son los favoritos o los deseados.
    public IReadOnlyList<VideojuegoDto> ObtenerWishlist() =>
        [.. _juegos.Where(juego =>
            juego.EsFavorito ||
            string.Equals(juego.Estado, "Deseado", StringComparison.OrdinalIgnoreCase))];

    public IReadOnlyList<VideojuegoDto> ObtenerTerminados() =>
        [.. _juegos.Where(juego => juego.Completado)];

    public Task<ResultadoCarga> AgregarAsync(VideojuegoDto nuevo, CancellationToken cancelacion = default) =>
        EjecutarAsync(async () =>
        {
            var creado = await _api.CrearAsync(nuevo, cancelacion);

            if (creado is not null)
            {
                // Los nuevos entran al principio, igual que antes.
                _juegos.Insert(0, creado);
                Avisar();
            }
        });

    public Task<ResultadoCarga> AlternarFavoritoAsync(int id, CancellationToken cancelacion = default) =>
        CambiarBanderasAsync(id, juego => juego.EsFavorito = !juego.EsFavorito, cancelacion);

    public Task<ResultadoCarga> AlternarCompletadoAsync(int id, CancellationToken cancelacion = default) =>
        CambiarBanderasAsync(id, juego => juego.Completado = !juego.Completado, cancelacion);

    public Task<ResultadoCarga> EliminarAsync(int id, CancellationToken cancelacion = default) =>
        EjecutarAsync(async () =>
        {
            if (await _api.EliminarAsync(id, cancelacion))
            {
                _juegos.RemoveAll(juego => juego.Id == id);
                Avisar();
            }
        });

    public IReadOnlyList<string> ObtenerPlataformas() => Catalogos.Plataformas;

    public IReadOnlyList<string> ObtenerEstados() => Catalogos.Estados;

    private Task<ResultadoCarga> CambiarBanderasAsync(
        int id,
        Action<VideojuegoDto> cambio,
        CancellationToken cancelacion) =>
        EjecutarAsync(async () =>
        {
            var juego = ObtenerPorId(id);

            if (juego is null)
            {
                return;
            }

            var copia = juego.Clonar();
            cambio(copia);

            var guardado = await _api.ActualizarBanderasAsync(
                id, copia.EsFavorito, copia.Completado, cancelacion);

            if (!guardado)
            {
                return;
            }

            var indice = _juegos.FindIndex(actual => actual.Id == id);

            if (indice >= 0)
            {
                _juegos[indice] = copia;
            }

            Avisar();
        });

    // Los mismos mensajes amigables que ya mostrabamos cuando el servicio fallaba.
    private static async Task<ResultadoCarga> EjecutarAsync(Func<Task> operacion)
    {
        try
        {
            await operacion();
            return ResultadoCarga.Ok();
        }
        catch (TaskCanceledException)
        {
            return ResultadoCarga.Fallo(
                "La solicitud tardó demasiado y se canceló. Revisa que GameVault.Api siga corriendo.");
        }
        catch (HttpRequestException ex)
        {
            return ResultadoCarga.Fallo(
                $"No se pudo contactar GameVault.Api en {ApiGameVault.BaseAddress}. {ex.Message}");
        }
        catch (JsonException)
        {
            return ResultadoCarga.Fallo(
                "El servicio respondió con un formato que la app no pudo interpretar.");
        }
    }

    private void Avisar() => CambioLaColeccion?.Invoke();
}
