using GameVault.Models;

namespace GameVault.Data;

/// <summary>
/// Datos de prueba hardcodeados de la Fase 1.
///
/// Las portadas apuntan a URLs reales y verificadas del CDN público de Steam y de
/// libretro-thumbnails. Se descartó Wikimedia: exige un User-Agent informativo y
/// responde 403 a HttpClient, que no manda ninguno, así que las portadas salían en
/// blanco. Cuando la Fase 4 conecte la API REST esta clase se elimina o queda como
/// fallback offline: nadie fuera de <see cref="VideojuegoRepository"/>
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
            ImagenUrl = "https://raw.githubusercontent.com/libretro-thumbnails/Nintendo_-_Wii_U/master/Named_Boxarts/Legend%20of%20Zelda,%20The%20-%20Breath%20of%20the%20Wild%20(USA)%20(En,Fr,Es).png",
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
            ImagenUrl = "https://raw.githubusercontent.com/libretro-thumbnails/Nintendo_-_Nintendo_64/master/Named_Boxarts/Legend%20of%20Zelda,%20The%20-%20Ocarina%20of%20Time%20(USA).png",
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
            ImagenUrl = "https://raw.githubusercontent.com/libretro-thumbnails/Sony_-_PlayStation_2/master/Named_Boxarts/Shadow%20of%20the%20Colossus%20(USA).png",
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
            ImagenUrl = "https://raw.githubusercontent.com/libretro-thumbnails/Nintendo_-_GameCube/master/Named_Boxarts/Metroid%20Prime%20(USA).png",
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
            ImagenUrl = "https://raw.githubusercontent.com/libretro-thumbnails/Nintendo_-_Super_Nintendo_Entertainment_System/master/Named_Boxarts/Chrono%20Trigger%20(USA).png",
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
            ImagenUrl = "https://raw.githubusercontent.com/libretro-thumbnails/Sony_-_PlayStation/master/Named_Boxarts/Final%20Fantasy%20VII%20(USA).png",
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
            ImagenUrl = "https://raw.githubusercontent.com/libretro-thumbnails/Sony_-_PlayStation_2/master/Named_Boxarts/Metal%20Gear%20Solid%203%20-%20Snake%20Eater%20(USA).png",
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
            ImagenUrl = "https://raw.githubusercontent.com/libretro-thumbnails/Nintendo_-_Nintendo_64/master/Named_Boxarts/Super%20Mario%2064%20(USA).png",
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
