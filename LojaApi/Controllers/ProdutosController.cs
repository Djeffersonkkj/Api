using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private static List<Produto> _produtos = [new Produto("Consolo 17cm", 290.99m), new("Libd gel", 69m)];

    [HttpGet()]
    public IActionResult ObterTodosProdutos()
        => Ok(_produtos.ToList());

    [HttpPost]
    public IActionResult CriarProduto(Produto produto)
    {
        _produtos.Add(produto);
        return Created();
    }
}
