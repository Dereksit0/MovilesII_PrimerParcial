using System.Text.Json.Serialization;

namespace GameVault.Models;

public class OfertaJuegoDto
{
    [JsonPropertyName("gameID")]
    public string? GameId { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("normalPrice")]
    public string? NormalPrice { get; set; }

    [JsonPropertyName("salePrice")]
    public string? SalePrice { get; set; }

    [JsonPropertyName("isOnSale")]
    public string? IsOnSale { get; set; }

    [JsonPropertyName("steamAppID")]
    public string? SteamAppId { get; set; }

    [JsonPropertyName("steamRatingText")]
    public string? SteamRatingText { get; set; }

    [JsonPropertyName("thumb")]
    public string? Thumb { get; set; }

    [JsonPropertyName("metacriticScore")]
    public string? MetacriticScore { get; set; }
}
