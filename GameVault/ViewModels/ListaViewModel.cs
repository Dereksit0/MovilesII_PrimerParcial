using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using GameVault.Data;
using GameVault.Models;

namespace GameVault.ViewModels;

public class ListaViewModel : BaseViewModel
{
    private readonly IVideojuegoRepository _repositorio;
    private static readonly CultureInfo Moneda = CultureInfo.GetCultureInfo("en-US");

    private int _totalJuegos;
    private int _totalCompletados;
    private string _valorTotal = "$0.00";

    public ListaViewModel(IVideojuegoRepository? repositorio = null)
    {
        _repositorio = repositorio ?? new VideojuegoRepository();
        TituloPantalla = "Mi colección";

        VerDetalleCommand = new Command<Videojuego>(async juego => await VerDetalleAsync(juego));
        AgregarCommand = new Command(async () => await AgregarAsync());
        RefrescarCommand = new Command(async () => await CargarAsync());
    }

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
