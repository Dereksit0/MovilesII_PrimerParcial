namespace GameVault;

/// <summary>
/// Nombres de ruta de Shell en un solo lugar.
///
/// Sin esto las rutas quedan como strings sueltos repartidos por los ViewModels y
/// cualquier error de tipeo se descubre en tiempo de ejecución. Las rutas absolutas
/// (con //) apuntan a los ShellContent declarados en AppShell.xaml; las relativas se
/// registran en AppShell.xaml.cs y se empujan sobre la pestaña activa, de modo que
/// el botón atrás vuelve solo.
/// </summary>
public static class AppRoutes
{
    /// <summary>Pestaña principal: toda la colección.</summary>
    public const string Lista = "//lista";

    /// <summary>Pestaña de deseados.</summary>
    public const string Wishlist = "//wishlist";

    /// <summary>Detalle de un juego. Espera el parámetro de consulta <c>id</c>.</summary>
    public const string Detalle = "detalle";

    /// <summary>
    /// Formulario. Sin parámetros equivale a "modo agregar"; con <c>id</c> abre en
    /// "modo editar".
    /// </summary>
    public const string Formulario = "formulario";

    /// <summary>Nombre del parámetro de consulta compartido por detalle y formulario.</summary>
    public const string ParametroId = "id";

    /// <summary>Construye "detalle?id=7".</summary>
    public static string DetalleDe(int id) => $"{Detalle}?{ParametroId}={id}";

    /// <summary>Construye "formulario?id=7" para editar un juego existente.</summary>
    public static string FormularioDe(int id) => $"{Formulario}?{ParametroId}={id}";
}
