 using Microsoft.EntityFrameworkCore;
using APINeoAlexandria.Models;

namespace APINeoAlexandria.Data;

public class TpFinalProgramacionContext : DbContext
{
    public TpFinalProgramacionContext(DbContextOptions options): base(options)
    {
    }

    public DbSet<Autores> Autores { get; set; }
    public DbSet<Carrito> Carritos { get; set; }
    public  DbSet<Categoria> Categoria { get; set; }
    public DbSet<DetalleCarrito> DetalleCarritos { get; set; }
    public DbSet<Libros> Libros { get; set; }
    public DbSet<Notas> Notas { get; set; }
    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<ValoraciondeUsuario> ValoraciondeUsuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Configuración de Autores
        modelBuilder.Entity<Autores>()
            .HasKey(a => a.IdAutor);

        // Configuración de Carrito
        modelBuilder.Entity<Carrito>()
            .HasKey(c => c.IdCarrito);

        // Configuración de Libros
        modelBuilder.Entity<Libros>()
            .HasKey(l => l.IdLibro);

        // Configuración de Usuarios
        modelBuilder.Entity<Usuarios>()
            .HasKey(u => u.IdUsuario);

        // Configuración de Notas
        modelBuilder.Entity<Notas>()
            .HasKey(n => n.IdNota);

        // Configuración de Valoraciones
        modelBuilder.Entity<ValoraciondeUsuario>()
            .HasKey(v => v.IdValoracion);

        // Forzar nombres de tablas en plural
        modelBuilder.Entity<Autores>().ToTable("Autores");
        modelBuilder.Entity<Carrito>().ToTable("Carrito");
        modelBuilder.Entity<Categoria>().ToTable("Categoria");
        modelBuilder.Entity<DetalleCarrito>().ToTable("DetalleCarrito");
        modelBuilder.Entity<Libros>().ToTable("Libros");
        modelBuilder.Entity<Notas>().ToTable("Notas");
        modelBuilder.Entity<Usuarios>().ToTable("Usuarios");
        modelBuilder.Entity<ValoraciondeUsuario>().ToTable("ValoraciondeUsuarios");
    }
}
