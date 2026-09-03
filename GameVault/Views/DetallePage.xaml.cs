using GameVault.ViewModels;

namespace GameVault.Views;

public partial class DetallePage : ContentPage, IQueryAttributable
{
    private readonly DetalleViewModel _viewModel;

    public DetallePage()
    {
        InitializeComponent();

        // Fase 2: llegará por DI (public DetallePage(DetalleViewModel vm)).
        _viewModel = new DetalleViewModel();
        BindingContext = _viewModel;
    }

    /// <summary>
    /// Shell entrega los parámetros de la query string a la página; se reenvían al
    /// ViewModel, que es quien sabe qué hacer con el Id. Así el code-behind no toca
    /// datos ni controles.
    /// </summary>
    public void ApplyQueryAttributes(IDictionary<string, object> query) =>
        _viewModel.ApplyQueryAttributes(query);

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}
