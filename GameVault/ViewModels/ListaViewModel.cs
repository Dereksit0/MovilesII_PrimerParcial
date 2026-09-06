using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        Juegos = repositorio.Videojuegos;
        Juegos.CollectionChanged += AlCambiarLaColeccion;
        ValorTotal = FormatoMoneda.Formatear(0m);
        ActualizarResumen();
    }

    public ObservableCollection<Videojuego> Juegos { get; }

    [ObservableProperty]
    public partial int TotalJuegos { get; set; }

    [ObservableProperty]
    public partial int TotalCompletados { get; set; }

    [ObservableProperty]
    public partial string ValorTotal { get; set; }

    [RelayCommand]
    private Task CargarAsync() => ObtenerDatosAsync(forzarRecarga: false);

    [RelayCommand]
    private Task RecargarAsync() => ObtenerDatosAsync(forzarRecarga: true);

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

        ActualizarResumen();
        IsBusy = false;
    }

    private void AlCambiarLaColeccion(object? remitente, NotifyCollectionChangedEventArgs argumentos) =>
        ActualizarResumen();

    private void ActualizarResumen()
    {
        TotalJuegos = Juegos.Count;
        TotalCompletados = Juegos.Count(juego => juego.Completado);
        ValorTotal = FormatoMoneda.Formatear(Juegos.Sum(juego => juego.ValorEstimado));
    }
}
