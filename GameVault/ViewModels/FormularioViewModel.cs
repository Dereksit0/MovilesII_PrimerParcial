using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameVault.Data;
using GameVault.Models;

namespace GameVault.ViewModels;

[QueryProperty(nameof(VideojuegoId), AppRoutes.ParametroId)]
public partial class FormularioViewModel : BaseViewModel
{
    private readonly IVideojuegoRepository _repositorio;

    private int _idEnEdicion;
    private bool _preparado;

    public FormularioViewModel(IVideojuegoRepository repositorio)
    {
        _repositorio = repositorio;

        Plataformas = repositorio.ObtenerPlataformas();
        Generos = repositorio.ObtenerGeneros();
        Estados = repositorio.ObtenerEstados();

        VideojuegoId = string.Empty;
        Titulo = string.Empty;
        ValorTexto = string.Empty;
        ImagenUrl = string.Empty;
        TituloPantalla = "Nuevo juego";
    }

    public IReadOnlyList<string> Plataformas { get; }

    public IReadOnlyList<string> Generos { get; }

    public IReadOnlyList<string> Estados { get; }

    [ObservableProperty]
    public partial string VideojuegoId { get; set; }

    [ObservableProperty]
    public partial string Titulo { get; set; }

    [ObservableProperty]
    public partial string? Plataforma { get; set; }

    [ObservableProperty]
    public partial string? Genero { get; set; }

    [ObservableProperty]
    public partial string? Estado { get; set; }

    [ObservableProperty]
    public partial string ValorTexto { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HayVistaPrevia))]
    public partial string ImagenUrl { get; set; }

    [ObservableProperty]
    public partial bool EsFavorito { get; set; }

    [ObservableProperty]
    public partial bool Completado { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoBotonGuardar))]
    public partial bool EsEdicion { get; set; }

    public bool HayVistaPrevia => !string.IsNullOrWhiteSpace(ImagenUrl);

    public string TextoBotonGuardar => EsEdicion ? "Guardar cambios" : "Agregar a la coleccion";

    [RelayCommand]
    private void Preparar()
    {
        if (_preparado)
        {
            return;
        }

        _preparado = true;
        MensajeError = null;

        if (!int.TryParse(VideojuegoId, out var id) || id <= 0)
        {
            EntrarEnModoAlta();
            return;
        }

        var juego = _repositorio.ObtenerPorId(id);

        if (juego is null)
        {
            EntrarEnModoAlta();
            MensajeError = "No se encontro el juego que se queria editar.";
            return;
        }

        _idEnEdicion = juego.Id;
        EsEdicion = true;
        TituloPantalla = "Editar juego";

        Titulo = juego.Titulo;
        Plataforma = juego.Plataforma;
        Genero = juego.Genero;
        Estado = juego.Estado;
        ValorTexto = juego.ValorEstimado.ToString("0.##", CultureInfo.InvariantCulture);
        ImagenUrl = juego.ImagenUrl;
        EsFavorito = juego.EsFavorito;
        Completado = juego.Completado;
    }

    [RelayCommand]
    private async Task GuardarAsync()
    {
        if (!TryValidar(out var valorEstimado))
        {
            return;
        }

        var juego = new Videojuego
        {
            Id = _idEnEdicion,
            Titulo = Titulo.Trim(),
            Plataforma = Plataforma!,
            Genero = Genero!,
            Estado = Estado!,
            ValorEstimado = valorEstimado,
            ImagenUrl = ImagenUrl.Trim(),
            EsFavorito = EsFavorito,
            Completado = Completado
        };

        if (EsEdicion)
        {
            _repositorio.Actualizar(juego);
        }
        else
        {
            _repositorio.Agregar(juego);
        }

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private static Task CancelarAsync() => Shell.Current.GoToAsync("..");

    partial void OnVideojuegoIdChanged(string value) => _preparado = false;

    private void EntrarEnModoAlta()
    {
        _idEnEdicion = 0;
        EsEdicion = false;
        TituloPantalla = "Nuevo juego";

        Titulo = string.Empty;
        Plataforma = null;
        Genero = null;
        Estado = null;
        ValorTexto = string.Empty;
        ImagenUrl = string.Empty;
        EsFavorito = false;
        Completado = false;
    }

    private bool TryValidar(out decimal valorEstimado)
    {
        valorEstimado = 0m;

        if (string.IsNullOrWhiteSpace(Titulo))
        {
            MensajeError = "El titulo es obligatorio.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Plataforma))
        {
            MensajeError = "Elige una plataforma.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Genero))
        {
            MensajeError = "Elige un genero.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Estado))
        {
            MensajeError = "Elige el estado dentro de la coleccion.";
            return false;
        }

        var texto = (ValorTexto ?? string.Empty).Trim().Replace(',', '.');

        if (!decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out valorEstimado) ||
            valorEstimado < 0m)
        {
            MensajeError = "El valor estimado debe ser un numero mayor o igual a cero.";
            return false;
        }

        MensajeError = null;
        return true;
    }
}
