using GameVault.ViewModels;

namespace GameVault.Views;

public partial class WishlistPage : ContentPage
{
    private readonly WishlistViewModel _viewModel;

    public WishlistPage()
    {
        InitializeComponent();

        // Fase 2: llegará por DI (public WishlistPage(WishlistViewModel vm)).
        _viewModel = new WishlistViewModel();
        BindingContext = _viewModel;
    }

    /// <summary>
    /// Ciclo de vida: refrescar en OnAppearing es lo que hace que, al marcar o
    /// desmarcar un juego como deseado desde el detalle, la wishlist ya esté
    /// actualizada al volver a esta pestaña.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}
