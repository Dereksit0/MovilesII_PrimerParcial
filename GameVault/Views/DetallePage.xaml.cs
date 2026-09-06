using GameVault.ViewModels;

namespace GameVault.Views;

public partial class DetallePage : ContentPage
{
    private readonly DetalleViewModel _viewModel;

    public DetallePage(DetalleViewModel viewModel)
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
