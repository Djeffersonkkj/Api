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
        var query = _dbContext.Tarefa.AsQueryable();

        if (concluida is not null)
            query = query.Where(t => t.Concluida == concluida);

        if (busca is not null)
            query = query.Where(t => t.Titulo.Contains(busca));

        return Ok(query.ToListAsync(cancellationToken));
    } 
            

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorIdAsync(CancellationToken cancellationToken, int id)
    {
        var tarefa = await _dbContext.Tarefa.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if(tarefa is null)
            return NotFound();

        return Ok(tarefa);
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync(CancellationToken cancellationToken, CriarTarefaDto dto)
    {
        _dbContext.Add(new Tarefa(dto.Titulo, dto.Concluida, dto.Descricao));
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarAsync(CancellationToken cancellationToken, int id, AtualizarTarefaDto dto)
    {
        var tarefa = await _dbContext.Tarefa.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if(tarefa is null)
            return NotFound();

        tarefa.Atualizar(dto.Titulo, dto.Descricao, dto.Concluida);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarAsync(CancellationToken cancellatiomToken, int id)
    {
        var tarefa = await _dbContext.Tarefa.FirstOrDefaultAsync(t => t.Id == id, cancellatiomToken);

        if(tarefa is null)
            return NotFound();

        _dbContext.Tarefa.Remove(tarefa);
        await _dbContext.SaveChangesAsync(cancellatiomToken);

        return NoContent();
    }

    [HttpPatch("{id}/concluir")]
    public async Task<IActionResult> ConcluirAsync(CancellationToken cancellationToken, int id)
    {
        var tarefa = await _dbContext.Tarefa.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if(tarefa is null)
            return NotFound();

        if(tarefa.Concluida == true)
            return BadRequest("Esta tarefa já está concluída");

        tarefa.Concluir();
        await _dbContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
