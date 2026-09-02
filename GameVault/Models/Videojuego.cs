using System.Globalization;

namespace GameVault.Models;

/// <summary>
/// Modelo principal del dominio: un videojuego dentro de la colección personal.
/// Es un POCO puro (sin INotifyPropertyChanged) porque en la Fase 4 estos objetos
/// se van a deserializar directamente desde una API REST.
/// </summary>
public class Videojuego
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    /// <summary>Consola o sistema donde se posee el juego. Ej: "PS5", "Nintendo Switch", "N64".</summary>
    public string Plataforma { get; set; } = string.Empty;

    /// <summary>Género principal. Ej: "RPG", "Shooter", "Plataformas", "Aventura", "Deportes".</summary>
    public string Genero { get; set; } = string.Empty;

    /// <summary>
    /// Estado dentro de la colección. Ej: "En colección", "Deseado",
    /// "Completo en caja (CIB)", "Solo cartucho/disco", "Vendido".
    /// </summary>
    public string Estado { get; set; } = string.Empty;

    /// <summary>Valor de mercado retro / reventa estimado, en USD.</summary>
    public decimal ValorEstimado { get; set; }

    /// <summary>Portada remota del juego (URL http/https).</summary>
    public string ImagenUrl { get; set; } = string.Empty;

    /// <summary>
    /// Fase 3 (multimedia): ruta en disco de la foto tomada con la cámara o elegida
    /// de la galería mediante MediaPicker. Hoy siempre es null; se declara desde ya
    /// para que <see cref="ImagenMostrada"/> resuelva la prioridad local-sobre-remota
    /// y la UI no tenga que cambiar cuando llegue esa fase.
    /// </summary>
    public string? ImagenLocalPath { get; set; }

    /// <summary>Marca el juego como deseado / wishlist.</summary>
    public bool EsFavorito { get; set; }

    /// <summary>Indica si el juego ya fue terminado.</summary>
    public bool Completado { get; set; }

    /// <summary>
    /// Fuente única que consume la UI para mostrar la portada: la foto local tiene
    /// prioridad sobre la portada remota. Las vistas siempre bindean a esta propiedad.
    /// </summary>
    public string ImagenMostrada =>
        string.IsNullOrWhiteSpace(ImagenLocalPath) ? ImagenUrl : ImagenLocalPath;

    /// <summary>Valor listo para mostrar, con símbolo de moneda y dos decimales.</summary>
    public string ValorFormateado =>
        ValorEstimado.ToString("C2", CultureInfo.GetCultureInfo("en-US"));

    /// <summary>Etiqueta de progreso usada en las tarjetas y en el detalle.</summary>
    public string EtiquetaProgreso => Completado ? "Completado" : "Pendiente";

    /// <summary>
    /// Copia superficial. La usa el formulario en modo edición para trabajar sobre un
    /// borrador: si el usuario cancela, el objeto original del repositorio queda intacto.
    /// </summary>
    public Videojuego Clonar() => (Videojuego)MemberwiseClone();
}
