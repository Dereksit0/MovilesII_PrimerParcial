using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using GameVault.Data;
using GameVault.Models;

namespace GameVault.ViewModels;

/// <summary>
/// ViewModel de la wishlist: los juegos con EsFavorito = true, es decir los que
/// todavía no se tienen y se quieren conseguir.
///
/// El filtro vive en el repositorio (GetFavoritosAsync) y no aquí, para que en la
/// Fase 4 pueda convertirse en un query contra la API sin tocar esta clase.
/// </summary>
public class WishlistViewModel : BaseViewModel
{
    private readonly IVideojuegoRepository _repositorio;
    private static readonly CultureInfo Moneda = CultureInfo.GetCultureInfo("en-US");

    private int _totalDeseados;
    private string _inversionEstimada = "$0.00";

    public WishlistViewModel(IVideojuegoRepository? repositorio = null)
    {
        _repositorio = repositorio ?? new VideojuegoRepository();
        TituloPantalla = "Wishlist";

        VerDetalleCommand = new Command<Videojuego>(async juego => await VerDetalleAsync(juego));
        RefrescarCommand = new Command(async () => await CargarAsync());
        AgregarCommand = new Command(async () => await AgregarAsync());
    }

    public ObservableCollection<Videojuego> Deseados { get; } = [];

    public ICommand VerDetalleCommand { get; }
    public ICommand RefrescarCommand { get; }
    public ICommand AgregarCommand { get; }

    public int TotalDeseados
    {
        get => _totalDeseados;
        private set => SetProperty(ref _totalDeseados, value);
    }

    /// <summary>Cuánto costaría completar la wishlist entera.</summary>
    public string InversionEstimada
    {
        get => _inversionEstimada;
        private set => SetProperty(ref _inversionEstimada, value);
    }

    public bool HayDeseados => Deseados.Count > 0;

    /// <summary>
    /// Ciclo de vida: al volver del detalle, donde se puede quitar o poner un juego
    /// en la wishlist, esta pantalla se vuelve a leer y la lista queda al día.
    /// </summary>
    public override Task OnAppearingAsync() => CargarAsync();

    private Task CargarAsync() => EjecutarAsync(async () =>
    {
        var deseados = await _repositorio.GetFavoritosAsync();

        Deseados.Clear();
        foreach (var juego in deseados)
        {
            Deseados.Add(juego);
        }

        TotalDeseados = deseados.Count;
        InversionEstimada = deseados.Sum(j => j.ValorEstimado).ToString("C2", Moneda);
        OnPropertyChanged(nameof(HayDeseados));
    },
    "No se pudo cargar la wishlist");

    private static async Task VerDetalleAsync(Videojuego? juego)
    {
        if (juego is null)
        {
            return;
        }

        await Shell.Current.GoToAsync(AppRoutes.DetalleDe(juego.Id));
    }

    private static Task AgregarAsync() => Shell.Current.GoToAsync(AppRoutes.Formulario);
}
