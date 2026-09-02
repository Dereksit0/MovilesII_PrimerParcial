using GameVault.Models;

namespace GameVault.Data;

/// <summary>
/// Datos de prueba hardcodeados de la Fase 1.
///
/// Las portadas apuntan a URLs reales y verificadas (CDN público de Steam y
/// Wikimedia Commons). Cuando la Fase 4 conecte la API REST, esta clase se elimina o
/// queda como fallback offline: nadie fuera de <see cref="VideojuegoRepository"/>
/// la referencia.
/// </summary>
public static class VideojuegoSeedData
{
    public static List<Videojuego> Crear() =>
    [
        new Videojuego
        {
            Id = 1,
            Titulo = "The Legend of Zelda: Breath of the Wild",
            Plataforma = "Nintendo Switch",
            Genero = "Aventura",
            Estado = "Completo en caja (CIB)",
            ValorEstimado = 59.99m,
            ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/c/c6/The_Legend_of_Zelda_Breath_of_the_Wild.jpg",
            EsFavorito = false,
            Completado = true
        },
        new Videojuego
        {
            Id = 2,
            Titulo = "Elden Ring",
            Plataforma = "PS5",
            Genero = "RPG",
            Estado = "En colección",
            ValorEstimado = 49.99m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1245620/library_600x900.jpg",
            EsFavorito = false,
            Completado = true
        },
        new Videojuego
        {
            Id = 3,
            Titulo = "The Legend of Zelda: Ocarina of Time",
            Plataforma = "N64",
            Genero = "Aventura",
            Estado = "Solo cartucho/disco",
            ValorEstimado = 85.00m,
            ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/5/57/The_Legend_of_Zelda_Ocarina_of_Time.jpg/330px-The_Legend_of_Zelda_Ocarina_of_Time.jpg",
            EsFavorito = false,
            Completado = true
        },
        new Videojuego
        {
            Id = 4,
            Titulo = "Shadow of the Colossus",
            Plataforma = "PS2",
            Genero = "Aventura",
            Estado = "Completo en caja (CIB)",
            ValorEstimado = 120.00m,
            ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/f/f8/Shadow_of_the_Colossus_%282005%29_cover.jpg",
            EsFavorito = false,
            Completado = true
        },
        new Videojuego
        {
            Id = 5,
            Titulo = "Metroid Prime",
            Plataforma = "GameCube",
            Genero = "Shooter",
            Estado = "Solo cartucho/disco",
            ValorEstimado = 95.50m,
            ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/b/ba/MetroidPrimebox.jpg",
            EsFavorito = false,
            Completado = false
        },
        new Videojuego
        {
            Id = 6,
            Titulo = "Chrono Trigger",
            Plataforma = "SNES",
            Genero = "RPG",
            Estado = "Deseado",
            ValorEstimado = 320.00m,
            ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/a/a7/Chrono_Trigger.jpg/330px-Chrono_Trigger.jpg",
            EsFavorito = true,
            Completado = false
        },
        new Videojuego
        {
            Id = 7,
            Titulo = "Final Fantasy VII",
            Plataforma = "PS1",
            Genero = "RPG",
            Estado = "Deseado",
            ValorEstimado = 140.00m,
            ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/c/c2/Final_Fantasy_VII_Box_Art.jpg",
            EsFavorito = true,
            Completado = false
        },
        new Videojuego
        {
            Id = 8,
            Titulo = "Metal Gear Solid 3: Snake Eater",
            Plataforma = "PS2",
            Genero = "Aventura",
            Estado = "Deseado",
            ValorEstimado = 110.00m,
            ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/b/b3/Mgs3box.jpg",
            EsFavorito = true,
            Completado = false
        },
        new Videojuego
        {
            Id = 9,
            Titulo = "Red Dead Redemption 2",
            Plataforma = "Xbox Series X",
            Genero = "Aventura",
            Estado = "En colección",
            ValorEstimado = 39.99m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/library_600x900.jpg",
            EsFavorito = false,
            Completado = true
        },
        new Videojuego
        {
            Id = 10,
            Titulo = "Hollow Knight",
            Plataforma = "PC",
            Genero = "Plataformas",
            Estado = "En colección",
            ValorEstimado = 14.99m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/367520/library_600x900.jpg",
            EsFavorito = false,
            Completado = true
        },
        new Videojuego
        {
            Id = 11,
            Titulo = "Hades",
            Plataforma = "Nintendo Switch",
            Genero = "RPG",
            Estado = "Deseado",
            ValorEstimado = 24.99m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1145360/library_600x900.jpg",
            EsFavorito = true,
            Completado = false
        },
        new Videojuego
        {
            Id = 12,
            Titulo = "Super Mario 64",
            Plataforma = "N64",
            Genero = "Plataformas",
            Estado = "Vendido",
            ValorEstimado = 75.00m,
            ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/e/e9/Super_Mario_64.png/330px-Super_Mario_64.png",
            EsFavorito = false,
            Completado = true
        },
        new Videojuego
        {
            Id = 13,
            Titulo = "God of War",
            Plataforma = "PS5",
            Genero = "Aventura",
            Estado = "Completo en caja (CIB)",
            ValorEstimado = 29.99m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1593500/library_600x900.jpg",
            EsFavorito = false,
            Completado = true
        },
        new Videojuego
        {
            Id = 14,
            Titulo = "Cyberpunk 2077",
            Plataforma = "PC",
            Genero = "Shooter",
            Estado = "En colección",
            ValorEstimado = 34.99m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1091500/library_600x900.jpg",
            EsFavorito = false,
            Completado = false
        },
        new Videojuego
        {
            Id = 15,
            Titulo = "Tony Hawk\u0027s Pro Skater 1 + 2",
            Plataforma = "PS5",
            Genero = "Deportes",
            Estado = "En colección",
            ValorEstimado = 27.99m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1079800/library_600x900.jpg",
            EsFavorito = false,
            Completado = false
        }
    ];
}
