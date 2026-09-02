namespace GameVault;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // GameVault tiene una identidad visual oscura fija: Colors.xaml define un
        // solo juego de valores (sin AppThemeBinding), así que se fuerza el tema
        // para que el sistema operativo no intente aplicar el claro.
        UserAppTheme = AppTheme.Dark;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell())
        {
            Title = "GameVault"
        };
    }
}
