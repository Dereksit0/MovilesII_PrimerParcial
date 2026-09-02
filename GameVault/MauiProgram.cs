using Microsoft.Extensions.Logging;

namespace GameVault;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // ------------------------------------------------------------------
        // Fase 2 (inyección de dependencias). Aquí van a registrarse el
        // repositorio, las páginas y los ViewModels:
        //
        //   builder.Services.AddSingleton<IVideojuegoRepository, VideojuegoRepository>();
        //   builder.Services.AddSingleton<ListaViewModel>();
        //   builder.Services.AddSingleton<ListaPage>();
        //   builder.Services.AddTransient<DetalleViewModel>();
        //   builder.Services.AddTransient<DetallePage>();
        //   ...
        //
        // Hoy cada página construye su propio ViewModel y cada ViewModel construye
        // su repositorio por defecto, pero los constructores ya reciben la interfaz
        // como parámetro opcional, así que el cambio se reduce a estas líneas.
        // ------------------------------------------------------------------

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
