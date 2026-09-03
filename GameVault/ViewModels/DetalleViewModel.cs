using System.Windows.Input;
using GameVault.Data;
using GameVault.Models;

namespace GameVault.ViewModels;

/// <summary>
/// ViewModel del detalle de un juego.
///
/// Implementa <see cref="IQueryAttributable"/> para recibir el Id que viaja en la
/// query string de Shell ("detalle?id=7"). El parámetro solo se guarda; la carga real
/// ocurre en OnAppearingAsync, de modo que al volver de editar el juego los datos se
/// releen y la pantalla muestra los cambios.
/// </summary>
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

    /// <summary>Texto del botón de wishlist, que cambia según el estado actual.</summary>
    public string TextoFavorito =>
        Juego?.EsFavorito == true ? "Quitar de la wishlist" : "Agregar a la wishlist";

    public ICommand EditarCommand { get; }
    public ICommand ToggleFavoritoCommand { get; }
    public ICommand VolverCommand { get; }

    /// <summary>
    /// Shell entrega aquí los parámetros de la query string. Llega antes de
    /// OnAppearing, así que basta con guardar el Id y dejar que la carga la haga el
    /// gancho de ciclo de vida.
    /// </summary>
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

        // El repositorio muta la misma instancia, así que la referencia no cambia y
        // SetProperty no dispararía nada: se notifica a mano para que las vistas
        // vuelvan a resolver las rutas que cuelgan de Juego.
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
