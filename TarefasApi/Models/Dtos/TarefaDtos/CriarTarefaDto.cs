using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarefasApi.Models.Dtos.TarefaDtos;

public sealed record class CriarTarefaDto
{
    [Required] [StringLength(80, MinimumLength = 3)]
    public string Titulo { get; set; }

    [StringLength(300)]
    public string Descricao { get; set; }

    public bool Concluida { get; set; }
}
