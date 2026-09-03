namespace GameVault;

public static class AppRoutes
{
    public const string Lista = "//lista";

    public const string Wishlist = "//wishlist";

    public const string Detalle = "detalle";

    public const string Formulario = "formulario";

    public const string ParametroId = "id";

    public static string DetalleDe(int id) => $"{Detalle}?{ParametroId}={id}";

    public static string FormularioDe(int id) => $"{Formulario}?{ParametroId}={id}";
}
