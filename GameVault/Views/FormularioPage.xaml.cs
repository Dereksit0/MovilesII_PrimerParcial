using GameVault.ViewModels;

namespace GameVault.Views;

public partial class FormularioPage : ContentPage, IQueryAttributable
{
    private readonly FormularioViewModel _viewModel;

    public FormularioPage()
    {
        InitializeComponent();

        _viewModel = new FormularioViewModel();
        BindingContext = _viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query) =>
        _viewModel.ApplyQueryAttributes(query);

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}
