using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameVault.Data;
using GameVault.Models;

namespace GameVault.ViewModels;

public partial class ListaViewModel : BaseViewModel
{
    private readonly IVideojuegoRepository _repositorio;

    public ListaViewModel(IVideojuegoRepository repositorio)
    {
        _repositorio = repositorio;
        TituloPantalla = "Mi colección";
        TextoBusqueda = string.Empty;
        TextoResultados = string.Empty;
        ValorTotal = FormatoMoneda.Formatear(0m);

        _repositorio.Videojuegos.CollectionChanged += AlCambiarLaColeccion;
        Refrescar();
    }

    public ObservableCollection<Videojuego> Juegos { get; } = [];

    [ObservableProperty]
    public partial int TotalJuegos { get; set; }

    [ObservableProperty]
    public partial int TotalCompletados { get; set; }

    [ObservableProperty]
    public partial string ValorTotal { get; set; }

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
        var todos = _repositorio.Videojuegos;

        TotalJuegos = todos.Count;
        TotalCompletados = todos.Count(juego => juego.Completado);
        ValorTotal = FormatoMoneda.Formatear(todos.Sum(juego => juego.ValorEstimado));

        var filtro = (TextoBusqueda ?? string.Empty).Trim();

        var visibles = string.IsNullOrEmpty(filtro)
            ? todos.ToList()
            : todos.Where(juego => Coincide(juego.Titulo, filtro)).ToList();

        Juegos.Clear();
        foreach (var juego in visibles)
        {
            Juegos.Add(juego);
        }

        TextoResultados = string.IsNullOrEmpty(filtro)
            ? string.Empty
            : $"{visibles.Count} de {todos.Count} juegos";
    }

    private static bool Coincide(string? texto, string filtro) =>
        CultureInfo.InvariantCulture.CompareInfo.IndexOf(
            texto ?? string.Empty,
            filtro,
            CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0;
}
