using CatalogoProdutos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoProdutos.Api.Data;

public class CatalogoContext : DbContext
{
    public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
}