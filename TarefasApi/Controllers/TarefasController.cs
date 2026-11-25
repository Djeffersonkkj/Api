using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Models;
using TarefasApi.Models.Dtos.TarefaDtos;

namespace TarefasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase
{
    public TarefasController(TarefaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly TarefaDbContext _dbContext;

    [HttpGet]
    public async Task<IActionResult> ObterTodasTarefasAsync(
        CancellationToken cancellationToken,
        [FromQuery] bool? concluida,
        [FromQuery] string busca)
    {
        var tarefas = await _dbContext.Tarefa.ToListAsync(cancellationToken);

        if (concluida is not null)
            tarefas = tarefas.Where(t => t.Concluida == concluida).ToList();

        if (busca is not null)
            tarefas = tarefas.Where(t => t.Titulo.Contains(busca)).ToList();

        return Ok(tarefas.ToList());
    } 
            

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorIdAsync(int id)
    {
        var tarefa = await _dbContext.Tarefa.FirstOrDefaultAsync(t => t.Id == id);

        if(tarefa is null)
            return NotFound();

        return Ok(tarefa);
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync(CriarTarefaDto dto)
    {
        _dbContext.Add(new Tarefa(dto.Titulo, dto.Concluida, dto.Descricao));
        await _dbContext.SaveChangesAsync();

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarAsync(int id, AtualizarTarefaDto dto)
    {
        var tarefa = await _dbContext.Tarefa.FirstOrDefaultAsync(t => t.Id == id);

        if(tarefa is null)
            return NotFound();

        tarefa.Atualizar(dto.Titulo, dto.Descricao, dto.Concluida);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarAsync(int id)
    {
        var tarefa = await _dbContext.Tarefa.FirstOrDefaultAsync(t => t.Id == id);

        if(tarefa is null)
            return NotFound();

        _dbContext.Tarefa.Remove(tarefa);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id}/concluir")]
    public async Task<IActionResult> ConcluirAsync(int id)
    {
        var tarefa = await _dbContext.Tarefa.FirstOrDefaultAsync(t => t.Id == id);

        if(tarefa is null)
            return NotFound();

        if(tarefa.Concluida == true)
            return BadRequest("Esta tarefa já está concluída");

        tarefa.Concluir();
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
