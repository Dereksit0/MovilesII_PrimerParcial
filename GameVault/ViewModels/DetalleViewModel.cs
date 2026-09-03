using System.Windows.Input;
using GameVault.Data;
using GameVault.Models;

namespace GameVault.ViewModels;

public class DetalleViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IVideojuegoRepository _repositorio;

    private int _juegoId;
    private Videojuego? _juego;

    public DetalleViewModel(IVideojuegoRepository? repositorio = null)
    {
        _repositorio = repositorio ?? new VideojuegoRepository();
        TituloPantalla = "Detalle";

        EditarCommand = new Command(async () => await EditarAsync(), () => HayJuego);
        ToggleFavoritoCommand = new Command(async () => await AlternarFavoritoAsync(), () => HayJuego);
        VolverCommand = new Command(async () => await VolverAsync());
    }

    public Videojuego? Juego
    {
        get => _juego;
        private set
        {
            if (SetProperty(ref _juego, value))
            {
                RefrescarDerivados();
            }
        }
    }

    public bool HayJuego => Juego is not null;

    public bool NoEncontrado => !HayJuego && !IsBusy;

    public string TextoFavorito =>
        Juego?.EsFavorito == true ? "Quitar de la wishlist" : "Agregar a la wishlist";

    public ICommand EditarCommand { get; }
    public ICommand ToggleFavoritoCommand { get; }
    public ICommand VolverCommand { get; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(AppRoutes.ParametroId, out var valor) &&
            int.TryParse(Convert.ToString(valor), out var id))
        {
            _juegoId = id;
        }
    }

    public override Task OnAppearingAsync() => CargarAsync();

    private Task CargarAsync() => EjecutarAsync(async () =>
    {
        Juego = await _repositorio.GetByIdAsync(_juegoId);

        if (Juego is null)
        {
            MensajeError = "No se encontró el juego solicitado.";
        }
    },
    "No se pudo cargar el juego");

    private Task EditarAsync() =>
        Juego is null ? Task.CompletedTask : Shell.Current.GoToAsync(AppRoutes.FormularioDe(Juego.Id));

    private async Task AlternarFavoritoAsync()
    {
        if (Juego is null)
        {
            return;
        }

        await _repositorio.ToggleFavoritoAsync(Juego.Id);

        RefrescarDerivados();
    }

    private static Task VolverAsync() => Shell.Current.GoToAsync("..");

    private void RefrescarDerivados()
    {
        OnPropertyChanged(nameof(Juego));
        OnPropertyChanged(nameof(HayJuego));
        OnPropertyChanged(nameof(NoEncontrado));
        OnPropertyChanged(nameof(TextoFavorito));
        ((Command)EditarCommand).ChangeCanExecute();
        ((Command)ToggleFavoritoCommand).ChangeCanExecute();
    }
}
