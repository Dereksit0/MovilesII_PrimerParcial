using GameVault.ViewModels;

namespace GameVault.Views;

public partial class FormularioPage : ContentPage, IQueryAttributable
{
    private readonly FormularioViewModel _viewModel;

    public FormularioPage()
    {
        InitializeComponent();

        // Fase 2: llegará por DI (public FormularioPage(FormularioViewModel vm)).
        _viewModel = new FormularioViewModel();
        BindingContext = _viewModel;
    }

    /// <summary>
    /// Sin parámetros la página queda en modo agregar; con "id" pasa a modo editar.
    /// La decisión la toma el ViewModel, aquí solo se reenvía la query string.
    /// </summary>
    public void ApplyQueryAttributes(IDictionary<string, object> query) =>
        _viewModel.ApplyQueryAttributes(query);

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}
