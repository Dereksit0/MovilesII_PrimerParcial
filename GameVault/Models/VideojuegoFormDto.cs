using System.ComponentModel.DataAnnotations;

namespace GameVault.Models;

// Lo que captura el formulario de alta. Va aparte del DTO que leemos del servicio
// porque aqui si queremos campos vacios mientras el usuario escribe.
// Cada campo de texto/numero lleva tres validaciones, cada una con su mensaje.
public class VideojuegoFormDto
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "El título debe tener entre 2 y 120 caracteres.")]
    [RegularExpression(@".*[\p{L}\p{N}].*", ErrorMessage = "El título debe llevar al menos una letra o un número.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Elige una plataforma.")]
    [EnCatalogo(nameof(Catalogos.Plataformas), ErrorMessage = "Esa plataforma no está en la lista.")]
    [StringLength(30, ErrorMessage = "La plataforma no puede pasar de 30 caracteres.")]
    public string? Plataforma { get; set; }

    [Required(ErrorMessage = "Elige el estado dentro de la colección.")]
    [EnCatalogo(nameof(Catalogos.Estados), ErrorMessage = "Ese estado no está en la lista.")]
    [StringLength(40, ErrorMessage = "El estado no puede pasar de 40 caracteres.")]
    public string? Estado { get; set; }

    [Required(ErrorMessage = "El valor estimado es obligatorio.")]
    [Range(0d, 100000d, ErrorMessage = "El valor estimado debe ir de 0 a 100,000.")]
    [MaximoDecimales(2, ErrorMessage = "El valor estimado solo puede tener hasta 2 decimales.")]
    public decimal? ValorEstimado { get; set; } = 0;

    [Range(0, 100, ErrorMessage = "El Metacritic va del 0 al 100.")]
    [RegularExpression(@"^\d{1,3}$", ErrorMessage = "El Metacritic debe ser un número entero de hasta 3 dígitos.")]
    public int? Metacritic { get; set; }

    [StringLength(400, ErrorMessage = "La URL de la portada es demasiado larga.")]
    [Url(ErrorMessage = "La portada tiene que ser una URL válida (https://...).")]
    [RegularExpression(@"^https://\S+$", ErrorMessage = "La portada debe empezar con https:// y no llevar espacios.")]
    public string? ImagenUrl
    {
        // El campo es opcional: si lo dejan vacio lo guardamos como null para que
        // la validacion de URL no se queje de una cadena vacia.
        get => imagenUrl;
        set => imagenUrl = string.IsNullOrWhiteSpace(value) ? null : value;
    }

    private string? imagenUrl;

    public bool EsFavorito { get; set; }

    public bool Completado { get; set; }

    public VideojuegoDto AVideojuego() => new()
    {
        Titulo = Titulo.Trim(),
        Plataforma = Plataforma ?? string.Empty,
        Estado = Estado ?? string.Empty,
        ValorEstimado = ValorEstimado ?? 0m,
        Metacritic = Metacritic,
        ImagenUrl = (ImagenUrl ?? string.Empty).Trim(),
        EsFavorito = EsFavorito,
        Completado = Completado
    };
}
