using GameVault.ViewModels;

namespace GameVault.Views;

public partial class WishlistPage : ContentPage
{
    private readonly WishlistViewModel _viewModel;

    public WishlistPage(WishlistViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.CargarCommand.Execute(null);
    }
}
