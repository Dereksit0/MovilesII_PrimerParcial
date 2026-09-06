using GameVault.ViewModels;

namespace GameVault.Views;

public partial class FormularioPage : ContentPage
{
    private readonly FormularioViewModel _viewModel;

    public FormularioPage(FormularioViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.PrepararCommand.Execute(null);
    }
}
