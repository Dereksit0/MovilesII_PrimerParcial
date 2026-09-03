using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using GameVault.Data;
using GameVault.Models;

namespace GameVault.ViewModels;

/// <summary>
/// ViewModel de la pantalla principal: toda la colección más un resumen de
/// cuántos juegos hay, cuánto valen y cuántos están terminados.
/// </summary>
public class ListaViewModel : BaseViewModel
{
    private readonly IVideojuegoRepository _repositorio;
    private static readonly CultureInfo Moneda = CultureInfo.GetCultureInfo("en-US");

    private int _totalJuegos;
    private int _totalCompletados;
    private string _valorTotal = "$0.00";

    /// <summary>
    /// El repositorio entra por constructor aunque hoy tenga un valor por defecto:
    /// en la Fase 2 se borra el "?? new VideojuegoRepository()" y el contenedor de
    /// DI resuelve la dependencia sin tocar nada más de esta clase.
    /// </summary>
    public ListaViewModel(IVideojuegoRepository? repositorio = null)
    {
        _repositorio = repositorio ?? new VideojuegoRepository();
        TituloPantalla = "Mi colección";

        VerDetalleCommand = new Command<Videojuego>(async juego => await VerDetalleAsync(juego));
        AgregarCommand = new Command(async () => await AgregarAsync());
        RefrescarCommand = new Command(async () => await CargarAsync());
    }

    /// <summary>
    /// Colección observable: la CollectionView se entera sola de altas y bajas.
    /// Es de solo lectura a propósito, siempre se muta con Clear/Add.
    /// </summary>
    public ObservableCollection<Videojuego> Juegos { get; } = [];

    public ICommand VerDetalleCommand { get; }
    public ICommand AgregarCommand { get; }
    public ICommand RefrescarCommand { get; }

    public int TotalJuegos
    {
        get => _totalJuegos;
        private set => SetProperty(ref _totalJuegos, value);
    }

    public int TotalCompletados
    {
        get => _totalCompletados;
        private set => SetProperty(ref _totalCompletados, value);
    }

    public string ValorTotal
    {
        get => _valorTotal;
        private set => SetProperty(ref _valorTotal, value);
    }

    public bool HayJuegos => Juegos.Count > 0;

    /// <summary>
    /// Ciclo de vida: la página llama a esto desde OnAppearing, así que la lista se
    /// vuelve a leer cada vez que la pantalla aparece. Es lo que hace que un juego
    /// recién guardado en el formulario ya esté aquí al volver.
    /// </summary>
    public override Task OnAppearingAsync() => CargarAsync();

    private Task CargarAsync() => EjecutarAsync(async () =>
    {
        var juegos = await _repositorio.GetVideojuegosAsync();

        Juegos.Clear();
        foreach (var juego in juegos)
        {
            Juegos.Add(juego);
        }

        TotalJuegos = juegos.Count;
        TotalCompletados = juegos.Count(j => j.Completado);
        ValorTotal = juegos.Sum(j => j.ValorEstimado).ToString("C2", Moneda);
        OnPropertyChanged(nameof(HayJuegos));
    },
    "No se pudo cargar la colección");

    /// <summary>Navega al detalle pasando el Id por query string.</summary>
    private static async Task VerDetalleAsync(Videojuego? juego)
    {
        if (juego is null)
        {
            return;
        }

        await Shell.Current.GoToAsync(AppRoutes.DetalleDe(juego.Id));
    }

    /// <summary>Abre el formulario sin parámetros, es decir en modo agregar.</summary>
    private static Task AgregarAsync() => Shell.Current.GoToAsync(AppRoutes.Formulario);
}
