using GameVault.ViewModels;

namespace GameVault.Views;

public partial class ListaPage : ContentPage
{
    private readonly ListaViewModel _viewModel;

    public ListaPage()
    {
        InitializeComponent();

        _viewModel = new ListaViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}
