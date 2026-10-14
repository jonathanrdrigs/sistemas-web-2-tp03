// Aluno: Jonathan Rodrigues Feitosa
// Prontuário: CB3030822

using GerenciadorProdutos.Data;
using GerenciadorProdutos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorProdutos.Controllers;

public class ProdutosController : Controller
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Produtos
    public async Task<IActionResult> Index()
    {
        var produtos = await _context.Produtos.OrderBy(p => p.Nome).ToListAsync();
        return View(produtos);
    }

    // GET: /Produtos/Details/  (ou /produto/)
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null) return NotFound();

        return View(produto);
    }

    // GET: /Produtos/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Produtos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nome,Descricao,Preco,QuantidadeEstoque")] Produto produto)
    {
        if (!ModelState.IsValid) return View(produto);

        _context.Add(produto);
        await _context.SaveChangesAsync();

        TempData["Mensagem"] = "Produto cadastrado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Produtos/Edit/
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        return View(produto);
    }

    // POST: /Produtos/Edit/
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Preco,QuantidadeEstoque")] Produto produto)
    {
        if (id != produto.Id) return NotFound();
        if (!ModelState.IsValid) return View(produto);

        try
        {
            _context.Update(produto);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Produtos.Any(p => p.Id == produto.Id)) return NotFound();
            throw;
        }

        TempData["Mensagem"] = "Produto atualizado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Produtos/Delete/
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null) return NotFound();

        return View(produto);
    }

    // POST: /Produtos/Delete/
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            TempData["Mensagem"] = "Produto excluído com sucesso!";
        }

        return RedirectToAction(nameof(Index));
    }
}