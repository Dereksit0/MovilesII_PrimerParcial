using GameVault.Api.Models;

namespace GameVault.Api.Data;

// Los juegos son los mismos que la app traia del catalogo de Steam en el primer parcial,
// pero ahora viven en nuestra base y cada uno puede tener resenas.
public static class SembradorDeDatos
{
    private const string PortadaSteam = "https://cdn.cloudflare.steamstatic.com/steam/apps/{0}/library_600x900.jpg";

    public static void Sembrar(GameVaultContext contexto)
    {
        if (contexto.Videojuegos.Any())
        {
            return;
        }

        contexto.Videojuegos.AddRange(Construir());
        contexto.SaveChanges();
    }

    private static List<Videojuego> Construir() =>
    [
        Juego("Baldur's Gate 3", "PC", "En colección", 1399m, 96, 1086940, favorito: false, completado: true,
            Opinion("Derek", 10, "Me acabe los tres actos y todavia le quiero dar otra vuelta con otra clase.", 2026, 1, 18),
            Opinion("mariana93", 9, "El combate por turnos se siente justo, aunque el acto 3 me pesaba en la laptop.", 2026, 2, 3),
            Opinion("el_tavo", 10, "Lo mejor que jugue el año pasado, sin exagerar.", 2026, 2, 21),
            Opinion("andrea_rpg", 8, "Buenisimo pero muy largo si no tienes tiempo libre.", 2026, 3, 9)),

        Juego("ELDEN RING", "PS5", "En colección", 1299m, 94, 1245620, favorito: true, completado: true,
            Opinion("Santiago", 10, "Dificil pero nunca injusto, el mapa es una locura.", 2026, 1, 25),
            Opinion("lalo_gamer", 9, "Me falto que explicaran mas las mecanicas, lo demas impecable.", 2026, 2, 14),
            Opinion("retro_cesar", 10, "Los jefes son de otro nivel, Malenia me quito la vida.", 2026, 3, 1)),

        Juego("The Witcher 3: Wild Hunt", "PC", "En colección", 549m, 93, 292030, favorito: false, completado: true,
            Opinion("Omar", 9, "Las misiones secundarias son mejores que la historia principal de otros juegos.", 2025, 11, 30),
            Opinion("pau", 10, "Le meti 120 horas y me faltaron los DLC.", 2026, 1, 8),
            Opinion("kevin", 8, "El combate envejecio un poco pero vale muchisimo la pena.", 2026, 2, 17)),

        Juego("Red Dead Redemption 2", "PC", "En colección", 1099m, 93, 1174180, favorito: false, completado: true,
            Opinion("Derek", 9, "El capitulo 6 me dejo mal una semana.", 2026, 2, 6),
            Opinion("andrea_rpg", 9, "Lento al principio, despues no lo puedes soltar.", 2026, 3, 12)),

        Juego("Hades", "Nintendo Switch", "Completo en caja (CIB)", 489m, 93, 1145360, favorito: false, completado: true,
            Opinion("Santiago", 10, "Perfecto para jugar en ratos cortos, cada run se siente distinta.", 2026, 1, 11),
            Opinion("mariana93", 9, "La narrativa dentro de un roguelike funciona rarisimo de bien.", 2026, 2, 2),
            Opinion("el_tavo", 8, "Se repite despues de la victoria 10, pero ya le saque jugo.", 2026, 3, 5)),

        Juego("Hollow Knight", "Nintendo Switch", "En colección", 299m, 90, 367520, favorito: false, completado: true,
            Opinion("kevin", 9, "El mapa de Hallownest es enorme y no te lleva de la mano.", 2026, 1, 29),
            Opinion("Omar", 8, "Me perdi como tres horas en Deepnest y aun asi me gusto.", 2026, 2, 23)),

        Juego("Portal 2", "PC", "Completo en caja (CIB)", 199m, 95, 620, favorito: false, completado: true,
            Opinion("lalo_gamer", 10, "El cooperativo con un amigo es la mejor parte.", 2025, 12, 14),
            Opinion("pau", 9, "Corto pero no le sobra nada.", 2026, 1, 20)),

        Juego("Half-Life 2", "PC", "En colección", 149m, 96, 220, favorito: false, completado: true,
            Opinion("retro_cesar", 10, "Lo rejuego cada año y sigue aguantando.", 2026, 1, 3),
            Opinion("Derek", 9, "Ravenholm sigue dando miedo.", 2026, 2, 28)),

        Juego("God of War", "PS4", "En colección", 699m, 93, 1593500, favorito: false, completado: true,
            Opinion("Santiago", 10, "La camara sin cortes se nota muchisimo en las peleas.", 2026, 2, 9),
            Opinion("andrea_rpg", 9, "La relacion con Atreus es lo que sostiene todo.", 2026, 3, 15)),

        Juego("Divinity: Original Sin 2", "PC", "En colección", 799m, 93, 435150, favorito: false, completado: true,
            Opinion("Omar", 9, "Mas libre de lo que esperaba, puedes romper las misiones a proposito.", 2026, 1, 16),
            Opinion("kevin", 8, "La curva de dificultad del acto 2 esta brava.", 2026, 2, 26)),

        Juego("Marvel's Spider-Man Remastered", "PS5", "Deseado", 1199m, 87, 1817070, favorito: true, completado: false,
            Opinion("pau", 9, "Moverse por Nueva York nunca se hace aburrido.", 2026, 3, 3),
            Opinion("el_tavo", 7, "Las misiones de sigilo con Mary Jane me sacaban del ritmo.", 2026, 3, 18)),

        Juego("Cyberpunk 2077", "PC", "Deseado", 999m, 86, 1091500, favorito: true, completado: false,
            Opinion("lalo_gamer", 8, "Ya con los parches corre bien, Night City esta increible.", 2026, 2, 11),
            Opinion("mariana93", 7, "Todavia le vi bugs raros, pero la historia de Phantom Liberty jala.", 2026, 3, 7),
            Opinion("Derek", 8, "Me gusto mas de lo que pensaba despues de todo lo que se dijo.", 2026, 3, 20)),

        Juego("Forza Horizon 5", "Xbox Series X", "Deseado", 1099m, 92, 1551360, favorito: true, completado: false,
            Opinion("kevin", 9, "Mexico se ve hermoso y maneja riquisimo con volante.", 2026, 2, 19)),

        Juego("Cuphead", "Xbox Series X", "Deseado", 389m, 88, 268910, favorito: true, completado: false,
            Opinion("retro_cesar", 9, "La animacion dibujada a mano es arte, los jefes son castigo puro.", 2026, 1, 22),
            Opinion("andrea_rpg", 7, "Muy dificil para mi, lo deje en el mundo 2.", 2026, 2, 27)),

        Juego("Stardew Valley", "PC", "En colección", 249m, 89, 413150, favorito: false, completado: false,
            Opinion("mariana93", 10, "Mi juego de bajar el estres, llevo tres granjas.", 2026, 1, 6),
            Opinion("Santiago", 9, "No pensaba que me iba a atrapar un juego de granja.", 2026, 2, 16)),

        Juego("Undertale", "PC", "Solo cartucho/disco", 159m, 92, 391540, favorito: false, completado: true,
            Opinion("el_tavo", 10, "La ruta pacifista vale cada minuto.", 2025, 12, 28),
            Opinion("Omar", 9, "Sans. Nada mas digo eso.", 2026, 2, 8)),

        Juego("Celeste", "Nintendo Switch", "Solo cartucho/disco", 279m, 91, 504230, favorito: false, completado: true,
            Opinion("pau", 10, "El control es exacto, cuando mueres sabes que fue tu culpa.", 2026, 1, 14),
            Opinion("kevin", 9, "Los capitulos B-side son otro juego completamente.", 2026, 3, 10)),

        Juego("Monster Hunter: World", "PS4", "En colección", 599m, 88, 582010, favorito: false, completado: false,
            Opinion("lalo_gamer", 8, "Mucho mejor acompañado, solo se hace pesado el grind.", 2026, 2, 13)),

        Juego("Terraria", "PC", "En colección", 189m, 83, 105600, favorito: false, completado: false,
            Opinion("retro_cesar", 9, "Parece chiquito y tiene contenido para meses.", 2026, 1, 31)),

        Juego("Grand Theft Auto V", "PS4", "Vendido", 0m, 96, 271590, favorito: false, completado: true,
            Opinion("Derek", 8, "Lo termine y lo vendi, el online no era para mi.", 2025, 11, 20),
            Opinion("andrea_rpg", 9, "Los tres protagonistas funcionan mejor de lo que suena.", 2026, 1, 9))
    ];

    private static Videojuego Juego(
        string titulo,
        string plataforma,
        string estado,
        decimal valor,
        int metacritic,
        int steamAppId,
        bool favorito,
        bool completado,
        params Resena[] resenas) =>
        new()
        {
            Titulo = titulo,
            Plataforma = plataforma,
            Estado = estado,
            ValorEstimado = valor,
            Metacritic = metacritic,
            ImagenUrl = string.Format(PortadaSteam, steamAppId),
            EsFavorito = favorito,
            Completado = completado,
            Resenas = resenas
        };

    private static Resena Opinion(string autor, int calificacion, string comentario, int anio, int mes, int dia) =>
        new()
        {
            Autor = autor,
            Calificacion = calificacion,
            Comentario = comentario,
            Fecha = new DateTimeOffset(anio, mes, dia, 12, 0, 0, TimeSpan.Zero)
        };
}
