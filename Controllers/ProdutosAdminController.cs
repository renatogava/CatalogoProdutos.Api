using CatalogoProdutos.Api.Data;
using CatalogoProdutos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/admin/produtos")]
public class ProdutosAdminController : ControllerBase
{
    private readonly CatalogoContext _context;

    public ProdutosAdminController(CatalogoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var produtos = await _context.Produtos.ToListAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        
        if (produto == null)
        {
            return NotFound();
        }

        return Ok(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(BuscarPorId), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, Produto produto)
    {
        if (id != produto.Id)
        {
            return BadRequest();
        }

        _context.Entry(produto).State = EntityState.Modified;
  
        await _context.SaveChangesAsync();
       
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        
        if (produto == null)
        {
            return NotFound();
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}