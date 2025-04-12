using FCG.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Jogo> Jogos => Set<Jogo>();
    public DbSet<Promocao> Promocoes => Set<Promocao>();
    public DbSet<Compra> Compras => Set<Compra>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relacionamento Jogo ? Promoções
        modelBuilder.Entity<Jogo>()
            .HasMany(j => j.Promocoes)
            .WithOne(p => p.Jogo!)
            .HasForeignKey(p => p.JogoId);

        // Relacionamento Compra ? Usuario
        modelBuilder.Entity<Compra>()
            .HasOne(c => c.Usuario)
            .WithMany(u => u.Compras)
            .HasForeignKey(c => c.UsuarioId);

        // Relacionamento Compra ? Jogo
        modelBuilder.Entity<Compra>()
            .HasOne(c => c.Jogo)
            .WithMany(j => j.Compras)
            .HasForeignKey(c => c.JogoId);
    }
}