namespace GameVault.Models;

public sealed record ResultadoCarga(bool Exito, string? MensajeError)
{
    public static ResultadoCarga Ok() => new(true, null);

    public static ResultadoCarga Fallo(string mensaje) => new(false, mensaje);
}
