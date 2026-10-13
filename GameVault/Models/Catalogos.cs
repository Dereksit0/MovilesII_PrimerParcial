namespace GameVault.Models;

// Las listas que ofrece el formulario. Estan aqui para que el repositorio las muestre
// en los selects y la validacion revise contra las mismas.
public static class Catalogos
{
    public static readonly string[] Plataformas =
    [
        "PC", "PS5", "PS4", "PS2", "PS1", "Xbox Series X", "Xbox 360",
        "Nintendo Switch", "GameCube", "N64", "SNES"
    ];

    public static readonly string[] Estados =
    [
        "En colección", "Deseado", "Completo en caja (CIB)",
        "Solo cartucho/disco", "Vendido"
    ];
}
