using System.ComponentModel.DataAnnotations;

namespace GameVault.Api.Models;

public class Resena
{
    public int Id { get; set; }

    public int VideojuegoId { get; set; }

    [Required(ErrorMessage = "La resena necesita un autor.")]
    [StringLength(80)]
    public string Autor { get; set; } = string.Empty;

    [Range(1, 10, ErrorMessage = "La calificacion va del 1 al 10.")]
    public int Calificacion { get; set; }

    [StringLength(600)]
    public string Comentario { get; set; } = string.Empty;

    public DateTimeOffset Fecha { get; set; }

    // Lado "uno" de la relacion.
    public Videojuego? Videojuego { get; set; }
}
