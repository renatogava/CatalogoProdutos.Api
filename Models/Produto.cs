

using Microsoft.EntityFrameworkCore;

namespace CatalogoProdutos.Api.Models;

public class Produto
{
    public int Id { get; set; }

    public string Nome { get; set; }

    [Precision(18, 2)]
    public decimal Preco { get; set; }

    public string Categoria { get; set; }

    public string ImagemUrl { get; set; }

    public bool Disponivel { get; set; }
}