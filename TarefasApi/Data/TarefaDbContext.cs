using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TarefasApi.Models;

namespace TarefasApi.Data;

public class TarefaDbContext : DbContext
{
    public TarefaDbContext(DbContextOptions<TarefaDbContext> options) : base(options)
    {
    }
    
    public DbSet<Tarefa> Tarefa { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Tarefas");
    
        modelBuilder.Entity<Tarefa>(entity =>
        {
            entity.ToTable(nameof(Tarefa));
        
            entity.HasKey(e => e.Id);
        
            entity.Property(e => e.Titulo)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Descricao)
                .HasMaxLength(500);
        });
        
    }


}
