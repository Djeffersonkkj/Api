using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaApi.Models;

public class Produto
{
    public Produto(string nome, decimal valor)
    {
        Nome = nome;
        Valor = valor;
    }

    public string Nome { get; set; }
    public decimal Valor { get; set; }
}
