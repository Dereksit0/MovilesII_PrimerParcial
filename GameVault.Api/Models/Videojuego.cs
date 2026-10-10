using System.ComponentModel.DataAnnotations;

namespace GameVault.Api.Models;

public class Videojuego
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El titulo es obligatorio.")]
    [StringLength(120, MinimumLength = 2)]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La plataforma es obligatoria.")]
    [StringLength(40)]
    public string Plataforma { get; set; } = string.Empty;

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [StringLength(40)]
    public string Estado { get; set; } = string.Empty;

    [Range(0, 100000)]
    public decimal ValorEstimado { get; set; }

    [Range(0, 100)]
    public int? Metacritic { get; set; }

    [StringLength(400)]
    public string ImagenUrl { get; set; } = string.Empty;

    public bool EsFavorito { get; set; }

    public bool Completado { get; set; }

    // Lado "muchos" de la relacion: un videojuego acumula varias resenas.
    public ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
