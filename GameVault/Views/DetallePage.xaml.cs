using GameVault.ViewModels;

namespace GameVault.Views;

public partial class DetallePage : ContentPage, IQueryAttributable
{
    private readonly DetalleViewModel _viewModel;

    public DetallePage()
    {
        InitializeComponent();

        _viewModel = new DetalleViewModel();
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
