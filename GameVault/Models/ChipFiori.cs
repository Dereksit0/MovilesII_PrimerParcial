namespace GameVault.Models;

// Traduce los estados del dominio al color semantico que les toca en Fiori.
// Asi el mismo estado se ve igual en la lista, en el panel y en el detalle.
public static class ChipFiori
{
    public static string DeEstado(string? estado) => estado switch
    {
        "En colección" => "chip chip-informativo",
        "Deseado" => "chip chip-critico",
        "Completo en caja (CIB)" => "chip chip-positivo",
        "Solo cartucho/disco" => "chip chip-neutral",
        "Vendido" => "chip chip-negativo",
        _ => "chip chip-neutral"
    };

    public static string DeProgreso(bool completado) =>
        completado ? "chip chip-positivo" : "chip chip-neutral";

    public static string DeMetacritic(int? metacritic) => metacritic switch
    {
        >= 90 => "chip chip-positivo",
        >= 75 => "chip chip-informativo",
        > 0 => "chip chip-critico",
        _ => "chip chip-neutral"
    };

    public static string DeCalificacion(double promedio) => promedio switch
    {
        >= 9 => "chip chip-positivo",
        >= 7 => "chip chip-informativo",
        > 0 => "chip chip-critico",
        _ => "chip chip-neutral"
    };
}
