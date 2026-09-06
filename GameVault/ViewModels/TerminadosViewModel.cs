using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameVault.Data;
using GameVault.Models;

namespace GameVault.ViewModels;

public partial class TerminadosViewModel : BaseViewModel
{
    private readonly IVideojuegoRepository _repositorio;

    public TerminadosViewModel(IVideojuegoRepository repositorio)
    {
        _repositorio = repositorio;
        TituloPantalla = "Terminados";
        TextoBusqueda = string.Empty;
        TextoResultados = string.Empty;
        ValorTerminado = FormatoMoneda.Formatear(0m);
        PorcentajeTexto = "0%";

        _repositorio.Videojuegos.CollectionChanged += AlCambiarLaColeccion;
        Refrescar();
    }

    public ObservableCollection<Videojuego> Terminados { get; } = [];

    [ObservableProperty]
    public partial int TotalTerminados { get; set; }

    [ObservableProperty]
    public partial string ValorTerminado { get; set; }

    [ObservableProperty]
    public partial string PorcentajeTexto { get; set; }

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

        var terminados = todos
            .Where(juego => juego.Completado)
            .OrderByDescending(juego => juego.Metacritic ?? 0)
            .ToList();

        TotalTerminados = terminados.Count;
        ValorTerminado = FormatoMoneda.Formatear(terminados.Sum(juego => juego.ValorEstimado));

        var porcentaje = todos.Count == 0
            ? 0d
            : (double)terminados.Count / todos.Count * 100d;

        PorcentajeTexto = porcentaje.ToString("0", CultureInfo.InvariantCulture) + "%";

        var filtro = (TextoBusqueda ?? string.Empty).Trim();

        var visibles = string.IsNullOrEmpty(filtro)
            ? terminados
            : terminados.Where(juego => Coincide(juego.Titulo, filtro)).ToList();

        Terminados.Clear();
        foreach (var juego in visibles)
        {
            Terminados.Add(juego);
        }

        TextoResultados = string.IsNullOrEmpty(filtro)
            ? string.Empty
            : $"{visibles.Count} de {terminados.Count} terminados";
    }

    private static bool Coincide(string? texto, string filtro) =>
        CultureInfo.InvariantCulture.CompareInfo.IndexOf(
            texto ?? string.Empty,
            filtro,
            CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0;
}
