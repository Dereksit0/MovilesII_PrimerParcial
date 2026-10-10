using System.Globalization;

namespace GameVault.Models;

// Lo que nos manda /odata/Resenas (o lo que viene dentro del $expand de un videojuego).
public class ResenaDto
{
    public int Id { get; set; }

    public int VideojuegoId { get; set; }

    public string Autor { get; set; } = string.Empty;

    public int Calificacion { get; set; }

    public string Comentario { get; set; } = string.Empty;

    public DateTimeOffset Fecha { get; set; }

    public string CalificacionTexto => $"{Calificacion}/10";

    public string FechaCorta => Fecha.ToString("dd MMM yyyy", FormatoMoneda.Cultura);

    public string Iniciales =>
        string.IsNullOrWhiteSpace(Autor)
            ? "?"
            : Autor.Trim()[..1].ToUpper(CultureInfo.CurrentCulture);
}
