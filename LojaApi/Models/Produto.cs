using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaApi.Models;

public class Produto
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Escolha uma categoria")]
    public int IdCategoria { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Mome { get; set; }

    [Range(0.01, 99999999.99, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
    public int Estoque { get; set; }

    public DateTimeOffset DataCriacao { get; set; }
}
