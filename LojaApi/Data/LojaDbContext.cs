using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaApi.Data;

public class LojaDbContext : DbContext
{
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Produto> Produto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Loja");

        modelBuilder.Entity<Categoria>(entidade =>
        {
            entidade.ToTable(nameof(Categoria));
            entidade.Property(categoria => categoria.Nome).HasMaxLength(50).IsRequired();
            entidade.HasIndex(categoria => categoria.Nome).IsUnique();
        });

        modelBuilder.Entity<Produto>(entidade =>
        {
            entidade.ToTable(nameof(Produto));
            entidade.Property(produto => produto.Nome).HasMaxLength(100).IsRequired();
            entidade.Property(produto => produto.Preco).HasPrecision(10, 2);

            entidade.HasOne(produto => produto.Categoria)
                .WithMany()
                .HasForeignKey(produto => produto.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict);

            entidade.HasIndex(produto => produto.Nome);
        });
    }
}
