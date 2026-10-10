using System.Text.Json.Serialization;

namespace GameVault.Services;

// Envoltura generica de la respuesta estandar de OData:
// { "@odata.context": "...", "@odata.count": 20, "value": [ ... ] }
public class ODataResponse<T>
{
    [JsonPropertyName("@odata.context")]
    public string? Contexto { get; set; }

    [JsonPropertyName("@odata.count")]
    public int? Total { get; set; }

    [JsonPropertyName("@odata.nextLink")]
    public string? SiguientePagina { get; set; }

    [JsonPropertyName("value")]
    public List<T> Value { get; set; } = [];

    public bool HayDatos => Value.Count > 0;
}
