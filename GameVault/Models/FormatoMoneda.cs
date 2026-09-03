using System.Globalization;

namespace GameVault.Models;

public static class FormatoMoneda
{
    public static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-MX");

    public static string Formatear(decimal valor) => $"{valor.ToString("C2", Cultura)} MXN";
}
