namespace GameVault.Models;

public class Videojuego
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Plataforma { get; set; } = string.Empty;

    public string Genero { get; set; } = string.Empty;

    public string Estado { get; set; } = string.Empty;

    public decimal ValorEstimado { get; set; }

    public string ImagenUrl { get; set; } = string.Empty;

    public string? ImagenLocalPath { get; set; }

    public bool EsFavorito { get; set; }

    public bool Completado { get; set; }

    public string ImagenMostrada =>
        string.IsNullOrWhiteSpace(ImagenLocalPath) ? ImagenUrl : ImagenLocalPath;

    public string ValorFormateado =>
        FormatoMoneda.Formatear(ValorEstimado);

    public string EtiquetaProgreso => Completado ? "Completado" : "Pendiente";

    public Videojuego Clonar() => (Videojuego)MemberwiseClone();
}
