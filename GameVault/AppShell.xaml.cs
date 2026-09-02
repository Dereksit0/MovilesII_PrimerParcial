using GameVault.Views;

namespace GameVault;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegistrarRutas();
    }

    /// <summary>
    /// Rutas que no son pestañas: se apilan sobre la pestaña activa, así que Shell
    /// genera solo el botón de retroceso y la navegación funciona en ambos sentidos.
    /// Registrarlas aquí es lo que permite invocarlas con
    /// <c>Shell.Current.GoToAsync("detalle?id=7")</c> desde cualquier ViewModel.
    /// </summary>
    private static void RegistrarRutas()
    {
        Routing.RegisterRoute(AppRoutes.Detalle, typeof(DetallePage));
        Routing.RegisterRoute(AppRoutes.Formulario, typeof(FormularioPage));
    }
}
