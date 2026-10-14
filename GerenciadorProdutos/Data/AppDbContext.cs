using GerenciadorProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorProdutos.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; } = null!;
}