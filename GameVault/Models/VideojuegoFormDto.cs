using System.ComponentModel.DataAnnotations;

namespace GameVault.Models;

// Lo que captura el formulario de alta. Va aparte del DTO que leemos del servicio
// porque aqui si queremos campos vacios mientras el usuario escribe.
public class VideojuegoFormDto
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "El título debe tener entre 2 y 120 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Elige una plataforma.")]
    public string? Plataforma { get; set; }

    [Required(ErrorMessage = "Elige el estado dentro de la colección.")]
    public string? Estado { get; set; }

    [Range(0, 100000, ErrorMessage = "El valor estimado debe ir de 0 a 100,000.")]
    public decimal ValorEstimado { get; set; }

    [Range(0, 100, ErrorMessage = "El Metacritic va del 0 al 100.")]
    public int? Metacritic { get; set; }

    [StringLength(400, ErrorMessage = "La URL de la portada es demasiado larga.")]
    [Url(ErrorMessage = "La portada tiene que ser una URL válida (https://...).")]
    public string? ImagenUrl { get; set; }

    public bool EsFavorito { get; set; }

    public bool Completado { get; set; }

    public VideojuegoDto AVideojuego() => new()
    {
        Titulo = Titulo.Trim(),
        Plataforma = Plataforma ?? string.Empty,
        Estado = Estado ?? string.Empty,
        ValorEstimado = ValorEstimado,
        Metacritic = Metacritic,
        ImagenUrl = (ImagenUrl ?? string.Empty).Trim(),
        EsFavorito = EsFavorito,
        Completado = Completado
    };
}
