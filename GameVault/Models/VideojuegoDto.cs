namespace GameVault.Models;

// Lo que nos manda /odata/Videojuegos. Las resenas llegan solo cuando pedimos $expand=Resenas.
public class VideojuegoDto
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Plataforma { get; set; } = string.Empty;

    public string Estado { get; set; } = string.Empty;

    public decimal ValorEstimado { get; set; }

    public int? Metacritic { get; set; }

    public string ImagenUrl { get; set; } = string.Empty;

    public bool EsFavorito { get; set; }

    public bool Completado { get; set; }

    public List<ResenaDto> Resenas { get; set; } = [];

    // --- Calculadas a partir de la entidad relacionada ---

    public int TotalResenas => Resenas.Count;

    public double CalificacionPromedio =>
        Resenas.Count == 0
            ? 0d
            : Math.Round(Resenas.Average(resena => resena.Calificacion), 1);

    public bool TieneResenas => Resenas.Count > 0;

    public string PromedioTexto =>
        TieneResenas
            ? $"{CalificacionPromedio.ToString("0.0", FormatoMoneda.Cultura)}/10"
            : "Sin reseñas";

    public string TotalResenasTexto =>
        TotalResenas switch
        {
            0 => "Sin reseñas todavía",
            1 => "1 reseña",
            _ => $"{TotalResenas} reseñas"
        };

    // --- Reglas que ya traiamos del primer parcial ---

    public string ValorFormateado => FormatoMoneda.Formatear(ValorEstimado);

    public string EtiquetaProgreso => Completado ? "Completado" : "Pendiente";

    public bool TieneMetacritic => Metacritic is > 0;

    public string MetacriticTexto => Metacritic is > 0 ? $"Metacritic {Metacritic}" : string.Empty;

    public bool TienePortada => !string.IsNullOrWhiteSpace(ImagenUrl);

    public VideojuegoDto Clonar()
    {
        var copia = (VideojuegoDto)MemberwiseClone();
        copia.Resenas = [.. Resenas];
        return copia;
    }
}
