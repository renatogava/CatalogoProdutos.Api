using CatalogoProdutos.Api.Data;
using CatalogoProdutos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly CatalogoContext _context;

    public ProdutosController(CatalogoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var produtos = await _context.Produtos.ToListAsync();
        return Ok(produtos);
    }
}