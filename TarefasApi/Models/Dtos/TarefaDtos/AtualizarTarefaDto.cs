using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarefasApi.Models.Dtos.TarefaDtos;

public sealed record class AtualizarTarefaDto
{
    public string Titulo { get; set; }

    public string Descricao { get; set; }
}
