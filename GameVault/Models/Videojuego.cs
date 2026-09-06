namespace GameVault.Models;

public class Videojuego
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Plataforma { get; set; } = string.Empty;

    public string Estado { get; set; } = string.Empty;

    public decimal ValorEstimado { get; set; }

    public string? Valoracion { get; set; }

    public int? Metacritic { get; set; }

    public string ImagenUrl { get; set; } = string.Empty;

    public string Miniatura { get; set; } = string.Empty;

    public string? ImagenLocalPath { get; set; }

    public bool EsFavorito { get; set; }

    public bool Completado { get; set; }

    public string ImagenMostrada
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(ImagenLocalPath))
            {
                return ImagenLocalPath;
            }

            return string.IsNullOrWhiteSpace(ImagenUrl) ? Miniatura : ImagenUrl;
        }
    }

    public string ValorFormateado => FormatoMoneda.Formatear(ValorEstimado);

    public string EtiquetaProgreso => Completado ? "Completado" : "Pendiente";

    public bool TieneValoracion => !string.IsNullOrWhiteSpace(Valoracion);

    public bool TieneMetacritic => Metacritic is > 0;

    public string MetacriticTexto => Metacritic is > 0 ? $"Metacritic {Metacritic}" : string.Empty;

    public Videojuego Clonar() => (Videojuego)MemberwiseClone();
}
