using GameVault.Views;

namespace GameVault;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegistrarRutas();
    }

    private static void RegistrarRutas()
    {
        Routing.RegisterRoute(AppRoutes.Detalle, typeof(DetallePage));
        Routing.RegisterRoute(AppRoutes.Formulario, typeof(FormularioPage));
    }
}
