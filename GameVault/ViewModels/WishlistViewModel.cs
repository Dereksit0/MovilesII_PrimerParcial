using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameVault.Data;
using GameVault.Models;

namespace GameVault.ViewModels;

public partial class WishlistViewModel : BaseViewModel
{
    private readonly IVideojuegoRepository _repositorio;

    public WishlistViewModel(IVideojuegoRepository repositorio)
    {
        _repositorio = repositorio;
        TituloPantalla = "Wishlist";
        TextoBusqueda = string.Empty;
        TextoResultados = string.Empty;
        InversionEstimada = FormatoMoneda.Formatear(0m);

        _repositorio.Videojuegos.CollectionChanged += AlCambiarLaColeccion;
        Refrescar();
    }

    public ObservableCollection<Videojuego> Deseados { get; } = [];

    [ObservableProperty]
    public partial int TotalDeseados { get; set; }

    [ObservableProperty]
    public partial string InversionEstimada { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HayBusqueda))]
    public partial string TextoBusqueda { get; set; }

    [ObservableProperty]
    public partial string TextoResultados { get; set; }

    public bool HayBusqueda => !string.IsNullOrWhiteSpace(TextoBusqueda);

    [RelayCommand]
    private Task CargarAsync() => ObtenerDatosAsync(forzarRecarga: false);

    [RelayCommand]
    private Task RecargarAsync() => ObtenerDatosAsync(forzarRecarga: true);

    [RelayCommand]
    private void LimpiarBusqueda() => TextoBusqueda = string.Empty;

    [RelayCommand]
    private static Task VerDetalleAsync(Videojuego? juego) =>
        juego is null
            ? Task.CompletedTask
            : Shell.Current.GoToAsync(AppRoutes.DetalleDe(juego.Id));

    [RelayCommand]
    private static Task AgregarAsync() => Shell.Current.GoToAsync(AppRoutes.Formulario);

    private async Task ObtenerDatosAsync(bool forzarRecarga)
    {
        if (IsBusy)
        {
            return;
        }

        IsBusy = true;
        MensajeError = null;

        var resultado = await _repositorio.InicializarAsync(forzarRecarga);

        if (!resultado.Exito)
        {
            MensajeError = resultado.MensajeError;
        }

        Refrescar();
        IsBusy = false;
    }

    partial void OnTextoBusquedaChanged(string value) => Refrescar();

    private void AlCambiarLaColeccion(object? remitente, NotifyCollectionChangedEventArgs argumentos) =>
        Refrescar();

    private void Refrescar()
    {
        var favoritos = _repositorio.Videojuegos
            .Where(juego => juego.EsFavorito)
            .OrderByDescending(juego => juego.ValorEstimado)
            .ToList();

        TotalDeseados = favoritos.Count;
        InversionEstimada = FormatoMoneda.Formatear(favoritos.Sum(juego => juego.ValorEstimado));

        var filtro = (TextoBusqueda ?? string.Empty).Trim();

        var visibles = string.IsNullOrEmpty(filtro)
            ? favoritos
            : favoritos.Where(juego => Coincide(juego.Titulo, filtro)).ToList();

        Deseados.Clear();
        foreach (var juego in visibles)
        {
            Deseados.Add(juego);
        }

        TextoResultados = string.IsNullOrEmpty(filtro)
            ? string.Empty
            : $"{visibles.Count} de {favoritos.Count} deseados";
    }

    private static bool Coincide(string? texto, string filtro) =>
        CultureInfo.InvariantCulture.CompareInfo.IndexOf(
            texto ?? string.Empty,
            filtro,
            CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0;
}
