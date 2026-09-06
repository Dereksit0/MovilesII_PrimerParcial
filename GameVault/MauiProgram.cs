using System.Net.Http.Headers;
using GameVault.Data;
using GameVault.ViewModels;
using GameVault.Views;
using Microsoft.Extensions.DependencyInjection;
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

        builder.Services.AddSingleton(_ =>
        {
            var http = new HttpClient
            {
                BaseAddress = new Uri("https://www.cheapshark.com/"),
                Timeout = TimeSpan.FromSeconds(20)
            };

            http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GameVault", "1.0"));
            http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(proyecto-escolar-maui)"));
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return http;
        });

        builder.Services.AddSingleton<IVideojuegoRepository, VideojuegoRepository>();

        builder.Services.AddSingleton<WishlistViewModel>();

        builder.Services.AddTransient<ListaViewModel>();
        builder.Services.AddTransient<ListaPage>();
        builder.Services.AddTransient<DetalleViewModel>();
        builder.Services.AddTransient<DetallePage>();
        builder.Services.AddTransient<WishlistPage>();
        builder.Services.AddTransient<FormularioViewModel>();
        builder.Services.AddTransient<FormularioPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
