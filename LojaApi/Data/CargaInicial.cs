using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaApi.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace LojaApi.Data;

public static class CargaInicial
{
    private const string MsgBancoIndisponivel =
        "Carga inicial não executada ({Erro}). Confira se o PostgreSQL está rodando, se a senha do appsettings.json está certa e se os scripts de database/ foram aplicados.";

    private static readonly string[] Categorias = ["Informática", "Papelaria", "Escritório"];

    public static async Task ExecutarAsync(WebApplication app)
    {
        using var escopo = app.Services.CreateScope();
        var dbContext = escopo.ServiceProvider.GetRequiredService<LojaDbContext>();
        var logger = escopo.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(CargaInicial));

        try
        {
            var existentes = await dbContext.Categoria.Select(categoria => categoria.Nome).ToListAsync();

            var novas = Categorias
                .Where(nome => !existentes.Contains(nome, StringComparer.OrdinalIgnoreCase))
                .Select(nome => new Categoria { Nome = nome, DataCriacao = DateTimeOffset.UtcNow })
                .ToList();

            if (novas.Count > 0)
            {
                dbContext.Categoria.AddRange(novas);
                await dbContext.SaveChangesAsync();
            }

            logger.LogInformation("Carga inicial: categorias criadas: {Categorias}", string.Join(", ", novas.Select(categoria => categoria.Nome)));
        }
        catch (NpgsqlException exception)
        {
            logger.LogWarning(MsgBancoIndisponivel, exception.Message);
        }
    }
}