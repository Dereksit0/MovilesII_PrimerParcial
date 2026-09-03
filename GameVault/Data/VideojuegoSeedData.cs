using GameVault.Models;

namespace GameVault.Data;

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
            ValorEstimado = 1299.00m,
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
            ValorEstimado = 999.00m,
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
            ValorEstimado = 1600.00m,
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
            ValorEstimado = 2250.00m,
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
            ValorEstimado = 1790.00m,
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
            ValorEstimado = 6000.00m,
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
            ValorEstimado = 2600.00m,
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
            ValorEstimado = 2050.00m,
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
            ValorEstimado = 749.00m,
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
            ValorEstimado = 279.00m,
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
            ValorEstimado = 469.00m,
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
            ValorEstimado = 1400.00m,
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
            ValorEstimado = 559.00m,
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
            ValorEstimado = 649.00m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1091500/library_600x900.jpg",
            EsFavorito = false,
            Completado = false
        },
        new Videojuego
        {
            Id = 15,
            Titulo = "Tony Hawk\u0027s Pro Skater 2",
            Plataforma = "PS1",
            Genero = "Deportes",
            Estado = "Completo en caja (CIB)",
            ValorEstimado = 650.00m,
            ImagenUrl = "https://raw.githubusercontent.com/libretro-thumbnails/Sony_-_PlayStation/master/Named_Boxarts/Tony%20Hawk%27s%20Pro%20Skater%202%20(USA).png",
            EsFavorito = false,
            Completado = true
        },
        new Videojuego
        {
            Id = 16,
            Titulo = "Fortnite",
            Plataforma = "PC",
            Genero = "Shooter",
            Estado = "En colección",
            ValorEstimado = 0.00m,
            ImagenUrl = "https://is1-ssl.mzstatic.com/image/thumb/Purple211/v4/d7/72/b0/d772b082-f156-4022-aadf-90c63347508b/AppIcon-0-0-1x_U007epad-0-1-85-220.png/512x512bb.jpg",
            EsFavorito = false,
            Completado = false
        },
        new Videojuego
        {
            Id = 17,
            Titulo = "Gears 5",
            Plataforma = "Xbox Series X",
            Genero = "Shooter",
            Estado = "En colección",
            ValorEstimado = 599.00m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1097840/library_600x900.jpg",
            EsFavorito = false,
            Completado = true
        },
        new Videojuego
        {
            Id = 18,
            Titulo = "Halo: The Master Chief Collection",
            Plataforma = "Xbox Series X",
            Genero = "Shooter",
            Estado = "Completo en caja (CIB)",
            ValorEstimado = 749.00m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/976730/library_600x900.jpg",
            EsFavorito = false,
            Completado = true
        },
        new Videojuego
        {
            Id = 19,
            Titulo = "Grand Theft Auto V",
            Plataforma = "PS5",
            Genero = "Aventura",
            Estado = "En colección",
            ValorEstimado = 899.00m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/271590/library_600x900.jpg",
            EsFavorito = false,
            Completado = true
        },
        new Videojuego
        {
            Id = 20,
            Titulo = "Marvel\u0027s Spider-Man",
            Plataforma = "PS5",
            Genero = "Aventura",
            Estado = "Deseado",
            ValorEstimado = 1199.00m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1817070/library_600x900.jpg",
            EsFavorito = true,
            Completado = false
        },
        new Videojuego
        {
            Id = 21,
            Titulo = "EA SPORTS FC 26",
            Plataforma = "PS5",
            Genero = "Deportes",
            Estado = "En colección",
            ValorEstimado = 1499.00m,
            ImagenUrl = "https://is1-ssl.mzstatic.com/image/thumb/Purple221/v4/36/22/fd/3622fd10-54c3-8a4f-946f-e9ef1b8a8c16/AppIcon-0-0-1x_U007emarketing-0-11-0-0-85-220.png/512x512bb.jpg",
            EsFavorito = false,
            Completado = false
        },
        new Videojuego
        {
            Id = 22,
            Titulo = "Roblox",
            Plataforma = "PC",
            Genero = "Aventura",
            Estado = "En colección",
            ValorEstimado = 0.00m,
            ImagenUrl = "https://is1-ssl.mzstatic.com/image/thumb/Purple221/v4/73/f6/b9/73f6b9d7-6a5c-e9db-57d6-dd82f64038f3/AppIcon-0-0-1x_U007epad-0-1-0-85-220.png/512x512bb.jpg",
            EsFavorito = false,
            Completado = false
        },
        new Videojuego
        {
            Id = 23,
            Titulo = "The Forest",
            Plataforma = "PC",
            Genero = "Terror",
            Estado = "En colección",
            ValorEstimado = 199.00m,
            ImagenUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/242760/library_600x900.jpg",
            EsFavorito = false,
            Completado = true
        }
    ];
}
