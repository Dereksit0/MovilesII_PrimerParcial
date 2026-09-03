using GameVault.ViewModels;

namespace GameVault.Views;

public partial class ListaPage : ContentPage
{
    private readonly ListaViewModel _viewModel;

    public ListaPage()
    {
        InitializeComponent();

        // Fase 2: este ViewModel se recibirá por constructor desde el contenedor de
        // DI (public ListaPage(ListaViewModel vm)). Hoy se instancia aquí, pero la
        // vista sigue sin saber nada del repositorio ni de los datos.
        _viewModel = new ListaViewModel();
        BindingContext = _viewModel;
    }

    /// <summary>
    /// Ciclo de vida: OnAppearing se dispara cada vez que la página se muestra,
    /// incluido el regreso desde Detalle o desde el Formulario. Delegar en el
    /// ViewModel mantiene la lógica fuera del code-behind.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}
