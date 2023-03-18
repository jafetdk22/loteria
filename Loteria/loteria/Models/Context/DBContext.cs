using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using loteria.Models.Entities;

namespace loteria.Models.Context;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cartas> Cartas { get; set; }

    public virtual DbSet<Celdas> Celdas { get; set; }

    public virtual DbSet<Tableros> Tableros { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-UES8EM4;Database=Loteria;integrated security=True; Encrypt=True;TrustServerCertificate=True;");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cartas>(entity =>
        {
            entity.HasKey(e => e.IdCarta).HasName("PK__Cartas__D3C2E8F13C70247D");

            entity.Property(e => e.IdCarta)
                .ValueGeneratedNever()
                .HasColumnName("id_carta");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen");
        });

        modelBuilder.Entity<Celdas>(entity =>
        {
            entity.HasKey(e => e.IdCelda).HasName("PK__Celdas__69F291FA8404A668");

            entity.Property(e => e.IdCelda)
                .ValueGeneratedNever()
                .HasColumnName("id_celda");
            entity.Property(e => e.Columna).HasColumnName("columna");
            entity.Property(e => e.Fila).HasColumnName("fila");
            entity.Property(e => e.IdCarta).HasColumnName("id_carta");
            entity.Property(e => e.IdTablero).HasColumnName("id_tablero");

            entity.HasOne(d => d.IdCartaNavigation).WithMany(p => p.Celdas)
                .HasForeignKey(d => d.IdCarta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Celdas__id_carta__3B75D760");

            entity.HasOne(d => d.IdTableroNavigation).WithMany(p => p.Celdas)
                .HasForeignKey(d => d.IdTablero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Celdas__id_table__3C69FB99");
        });

        modelBuilder.Entity<Tableros>(entity =>
        {
            entity.HasKey(e => e.IdTablero).HasName("PK__Tableros__D41BC14927C18FE8");

            entity.Property(e => e.IdTablero)
                .ValueGeneratedNever()
                .HasColumnName("id_tablero");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
