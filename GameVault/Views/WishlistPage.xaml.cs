using GameVault.ViewModels;

namespace GameVault.Views;

public partial class WishlistPage : ContentPage
{
    private readonly WishlistViewModel _viewModel;

    public WishlistPage()
    {
        InitializeComponent();

        _viewModel = new WishlistViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}
