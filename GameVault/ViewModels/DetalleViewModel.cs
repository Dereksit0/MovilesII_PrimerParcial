using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameVault.Data;
using GameVault.Models;

namespace GameVault.ViewModels;

[QueryProperty(nameof(VideojuegoId), AppRoutes.ParametroId)]
public partial class DetalleViewModel : BaseViewModel
{
    private readonly IVideojuegoRepository _repositorio;

    public DetalleViewModel(IVideojuegoRepository repositorio)
    {
        _repositorio = repositorio;
        TituloPantalla = "Detalle";
        VideojuegoId = string.Empty;
    }

    [ObservableProperty]
    public partial string VideojuegoId { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HayJuego))]
    [NotifyPropertyChangedFor(nameof(NoEncontrado))]
    [NotifyPropertyChangedFor(nameof(TextoFavorito))]
    [NotifyCanExecuteChangedFor(nameof(EditarCommand))]
    [NotifyCanExecuteChangedFor(nameof(AlternarFavoritoCommand))]
    [NotifyCanExecuteChangedFor(nameof(EliminarCommand))]
    public partial Videojuego? Juego { get; set; }

    public bool HayJuego => Juego is not null;

    public bool NoEncontrado => !HayJuego && !IsBusy;

    public string TextoFavorito =>
        Juego?.EsFavorito == true ? "Quitar de la wishlist" : "Agregar a la wishlist";

    [RelayCommand]
    private void Cargar()
    {
        MensajeError = null;

        Juego = int.TryParse(VideojuegoId, out var id)
            ? _repositorio.ObtenerPorId(id)
            : null;

        if (Juego is null)
        {
            MensajeError = "No se encontró el juego solicitado.";
        }
    }

    [RelayCommand(CanExecute = nameof(HayJuego))]
    private Task EditarAsync() => Shell.Current.GoToAsync(AppRoutes.FormularioDe(Juego!.Id));

    [RelayCommand(CanExecute = nameof(HayJuego))]
    private void AlternarFavorito()
    {
        var actualizado = Juego!.Clonar();
        actualizado.EsFavorito = !actualizado.EsFavorito;

        _repositorio.Actualizar(actualizado);
        Juego = actualizado;
    }

    [RelayCommand(CanExecute = nameof(HayJuego))]
    private async Task EliminarAsync()
    {
        var confirmado = await Shell.Current.DisplayAlertAsync(
            "Eliminar juego",
            $"¿Seguro que quieres eliminar \"{Juego!.Titulo}\" de tu colección? Esta acción no se puede deshacer.",
            "Eliminar",
            "Cancelar");

        if (!confirmado)
        {
            return;
        }

        _repositorio.Eliminar(Juego.Id);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private static Task VolverAsync() => Shell.Current.GoToAsync("..");

    partial void OnVideojuegoIdChanged(string value) => CargarCommand.Execute(null);
}
