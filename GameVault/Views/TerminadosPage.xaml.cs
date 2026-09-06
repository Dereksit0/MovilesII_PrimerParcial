using GameVault.ViewModels;

namespace GameVault.Views;

public partial class TerminadosPage : ContentPage
{
    private readonly TerminadosViewModel _viewModel;

    public TerminadosPage(TerminadosViewModel viewModel)
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
