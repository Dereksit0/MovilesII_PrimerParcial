using System.ComponentModel.DataAnnotations;

namespace GameVault.Models;

// Revisa que el valor sea uno de los que ofrece el select (nadie debe colar una plataforma inventada).
[AttributeUsage(AttributeTargets.Property)]
public sealed class EnCatalogoAttribute : ValidationAttribute
{
    private readonly string[] _catalogo;

    public EnCatalogoAttribute(string nombreCatalogo)
    {
        _catalogo = nombreCatalogo switch
        {
            nameof(Catalogos.Plataformas) => Catalogos.Plataformas,
            nameof(Catalogos.Estados) => Catalogos.Estados,
            _ => []
        };
    }

    public override bool IsValid(object? value) =>
        value is null or "" || _catalogo.Contains(value.ToString());
}

// Para el dinero: maximo dos decimales.
[AttributeUsage(AttributeTargets.Property)]
public sealed class MaximoDecimalesAttribute : ValidationAttribute
{
    private readonly int _decimales;

    public MaximoDecimalesAttribute(int decimales)
    {
        _decimales = decimales;
    }

    public override bool IsValid(object? value) =>
        value is not decimal numero || Math.Round(numero, _decimales) == numero;
}
