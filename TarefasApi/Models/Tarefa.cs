using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarefasApi.Models;

public class Tarefa
{
    public Tarefa(int id, string titulo, bool concluida, string descricao = null)
    {
        Id = id;
        Titulo = titulo;
        Concluida = concluida;
        Descricao = descricao;
        DataCriacao = DateTime.Now;
    }

    public int Id { get; set; }

    [Required] [StringLength(80, MinimumLength = 3)]
    public string Titulo { get; set; }

    [StringLength(300)]
    public string Descricao { get; set; }

    public bool Concluida { get; set; }
    public DateTimeOffset DataCriacao { get; set; }

    public void Atualizar(string titulo, string descricao, bool concluida)
    {
        Titulo = titulo?? Titulo;
        Descricao = descricao?? Descricao;
        Concluida = concluida;
    }

    public void Concluir()
        => Concluida = true;
}
