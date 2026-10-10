namespace GameVault.Services;

// Donde vive GameVault.Api cuando la corremos en local.
// El emulador de Android no ve "localhost" de la maquina, por eso usa 10.0.2.2.
public static class ApiGameVault
{
    public const int Puerto = 5190;

    public const string RutaOData = "odata";

    public static Uri BaseAddress =>
        new(OperatingSystem.IsAndroid()
            ? $"http://10.0.2.2:{Puerto}/"
            : $"http://localhost:{Puerto}/");
}
