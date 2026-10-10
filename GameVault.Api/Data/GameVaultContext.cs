using GameVault.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Api.Data;

public class GameVaultContext : DbContext
{
    public GameVaultContext(DbContextOptions<GameVaultContext> opciones)
        : base(opciones)
    {
    }

    public DbSet<Videojuego> Videojuegos => Set<Videojuego>();

    public DbSet<Resena> Resenas => Set<Resena>();

    protected override void OnModelCreating(ModelBuilder modelo)
    {
        modelo.Entity<Videojuego>(juego =>
        {
            juego.HasKey(j => j.Id);
            juego.Property(j => j.Titulo).IsRequired().HasMaxLength(120);
            juego.Property(j => j.Plataforma).IsRequired().HasMaxLength(40);
            juego.Property(j => j.Estado).IsRequired().HasMaxLength(40);
            juego.Property(j => j.ValorEstimado).HasPrecision(10, 2);
            juego.Property(j => j.ImagenUrl).HasMaxLength(400);
        });

        modelo.Entity<Resena>(resena =>
        {
            resena.HasKey(r => r.Id);
            resena.Property(r => r.Autor).IsRequired().HasMaxLength(80);
            resena.Property(r => r.Comentario).HasMaxLength(600);

            // Uno a muchos: si se borra el juego se van tambien sus resenas.
            resena.HasOne(r => r.Videojuego)
                  .WithMany(j => j.Resenas)
                  .HasForeignKey(r => r.VideojuegoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
