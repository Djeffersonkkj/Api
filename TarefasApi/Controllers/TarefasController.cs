using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TarefasApi.Models;
using TarefasApi.Models.Dtos.TarefaDtos;

namespace TarefasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase
{
    private static List<Tarefa> _tarefas = [
        new Tarefa(1, "Chupar dedo do pé", false),
        new Tarefa(2, "Escovar o umbigo", false),
        new Tarefa(3, "Ler manifesto comunista", true),
        new Tarefa(4, "Tomar café", false)
    ];

    [HttpGet]
    public IActionResult ObterTodasTarefas(
        [FromQuery] bool? concluida,
        [FromQuery] string busca)
    {
        var tarefas = _tarefas.ToList();

        if (concluida is not null)
            tarefas = tarefas.Where(t => t.Concluida == concluida).ToList();

        if (busca is not null)
            tarefas = tarefas.Where(t => t.Titulo.Contains(busca)).ToList();

        return Ok(tarefas.ToList());
    } 
            

    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);

        if(tarefa is null)
            return NotFound();

        return Ok(tarefa);
    }

    [HttpPost]
    public IActionResult Criar(CriarTarefaDto dto)
    {
        int proximoId = Math.Max(_tarefas.Max(t => t.Id) + 1, 1);

        _tarefas.Add(new Tarefa(proximoId, dto.Titulo, dto.Concluida, dto.Descricao));

        return Created();
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, AtualizarTarefaDto dto)
    {
        var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);

        if(tarefa is null)
            return NotFound();

        tarefa.Atualizar(dto.Titulo, dto.Descricao, dto.Concluida);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);

        if(tarefa is null)
            return NotFound();

        _tarefas.Remove(tarefa);

        return NoContent();
    }

    [HttpPatch("{id}/concluir")]
    public IActionResult Concluir(int id)
    {
        var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);

        if(tarefa is null)
            return NotFound();

        if(tarefa.Concluida == true)
            return BadRequest("Esta tarefa já está concluída");

        tarefa.Concluir();

        return NoContent();
    }
}
