using System.Collections.ObjectModel;
using System.Windows.Input;
using GameVault.Data;
using GameVault.Models;

namespace GameVault.ViewModels;

public class WishlistViewModel : BaseViewModel
{
    private readonly IVideojuegoRepository _repositorio;

    private int _totalDeseados;
    private string _inversionEstimada = FormatoMoneda.Formatear(0m);

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

    public string InversionEstimada
    {
        get => _inversionEstimada;
        private set => SetProperty(ref _inversionEstimada, value);
    }

    public bool HayDeseados => Deseados.Count > 0;

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
        InversionEstimada = FormatoMoneda.Formatear(deseados.Sum(j => j.ValorEstimado));
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
