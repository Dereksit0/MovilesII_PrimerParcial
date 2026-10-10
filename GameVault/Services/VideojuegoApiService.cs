using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using GameVault.Models;

namespace GameVault.Services;

// Toda la plática con GameVault.Api pasa por aquí. Nada de datos inventados en el cliente.
public class VideojuegoApiService : IVideojuegoApiService
{
    private static readonly JsonSerializerOptions OpcionesJson = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _http;

    public VideojuegoApiService(HttpClient http)
    {
        _http = http;
    }

    public Task<IReadOnlyList<VideojuegoDto>> ObtenerTodosAsync(CancellationToken cancelacion = default) =>
        ObtenerListaAsync(
            $"{ApiGameVault.RutaOData}/Videojuegos?$expand=Resenas&$orderby=Titulo&$count=true",
            cancelacion);

    public Task<IReadOnlyList<VideojuegoDto>> ObtenerPorPlataformaAsync(
        string plataforma,
        CancellationToken cancelacion = default)
    {
        var filtro = Uri.EscapeDataString($"Plataforma eq '{plataforma.Replace("'", "''")}'");

        return ObtenerListaAsync(
            $"{ApiGameVault.RutaOData}/Videojuegos?$filter={filtro}&$expand=Resenas&$orderby=Titulo&$count=true",
            cancelacion);
    }

    public async Task<VideojuegoDto?> ObtenerPorIdAsync(int id, CancellationToken cancelacion = default)
    {
        using var respuesta = await _http.GetAsync(
            $"{ApiGameVault.RutaOData}/Videojuegos({id})?$expand=Resenas",
            cancelacion);

        if (respuesta.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        respuesta.EnsureSuccessStatusCode();

        await using var flujo = await respuesta.Content.ReadAsStreamAsync(cancelacion);
        return await JsonSerializer.DeserializeAsync<VideojuegoDto>(flujo, OpcionesJson, cancelacion);
    }

    public async Task<IReadOnlyList<ResenaDto>> ObtenerResenasAsync(
        int videojuegoId,
        CancellationToken cancelacion = default)
    {
        var filtro = Uri.EscapeDataString($"VideojuegoId eq {videojuegoId}");
        var ruta = $"{ApiGameVault.RutaOData}/Resenas?$filter={filtro}&$orderby=Fecha desc";

        using var respuesta = await _http.GetAsync(ruta, cancelacion);
        respuesta.EnsureSuccessStatusCode();

        await using var flujo = await respuesta.Content.ReadAsStreamAsync(cancelacion);
        var envoltura = await JsonSerializer.DeserializeAsync<ODataResponse<ResenaDto>>(
            flujo, OpcionesJson, cancelacion);

        return envoltura?.Value ?? [];
    }

    public async Task<VideojuegoDto?> CrearAsync(VideojuegoDto nuevo, CancellationToken cancelacion = default)
    {
        // Solo mandamos las columnas; las resenas se dan de alta por su propio endpoint.
        var cuerpo = new
        {
            nuevo.Titulo,
            nuevo.Plataforma,
            nuevo.Estado,
            nuevo.ValorEstimado,
            nuevo.Metacritic,
            nuevo.ImagenUrl,
            nuevo.EsFavorito,
            nuevo.Completado
        };

        using var respuesta = await _http.PostAsJsonAsync(
            $"{ApiGameVault.RutaOData}/Videojuegos",
            cuerpo,
            cancelacion);

        if (!respuesta.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"El servicio rechazó el alta ({(int)respuesta.StatusCode}).",
                null,
                respuesta.StatusCode);
        }

        await using var flujo = await respuesta.Content.ReadAsStreamAsync(cancelacion);
        return await JsonSerializer.DeserializeAsync<VideojuegoDto>(flujo, OpcionesJson, cancelacion);
    }

    public async Task<bool> ActualizarBanderasAsync(
        int id,
        bool esFavorito,
        bool completado,
        CancellationToken cancelacion = default)
    {
        var cambios = new { EsFavorito = esFavorito, Completado = completado };

        using var peticion = new HttpRequestMessage(
            HttpMethod.Patch,
            $"{ApiGameVault.RutaOData}/Videojuegos({id})")
        {
            Content = JsonContent.Create(cambios)
        };

        using var respuesta = await _http.SendAsync(peticion, cancelacion);
        return respuesta.IsSuccessStatusCode;
    }

    public async Task<bool> EliminarAsync(int id, CancellationToken cancelacion = default)
    {
        using var respuesta = await _http.DeleteAsync(
            $"{ApiGameVault.RutaOData}/Videojuegos({id})",
            cancelacion);

        return respuesta.IsSuccessStatusCode;
    }

    private async Task<IReadOnlyList<VideojuegoDto>> ObtenerListaAsync(string ruta, CancellationToken cancelacion)
    {
        using var respuesta = await _http.GetAsync(ruta, cancelacion);
        respuesta.EnsureSuccessStatusCode();

        await using var flujo = await respuesta.Content.ReadAsStreamAsync(cancelacion);
        var envoltura = await JsonSerializer.DeserializeAsync<ODataResponse<VideojuegoDto>>(
            flujo, OpcionesJson, cancelacion);

        return envoltura?.Value ?? [];
    }
}
