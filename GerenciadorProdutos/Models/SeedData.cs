// Aluno: Jonathan Rodrigues Feitosa
// Prontuário: CB3030822

using GerenciadorProdutos.Data;

namespace GerenciadorProdutos.Models;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Produtos.Any()) return;

        context.Produtos.AddRange(
            new Produto { Nome = "Teclado Mecânico", Descricao = "Teclado mecânico ABNT2 com switch azul.", Preco = 249.90m, QuantidadeEstoque = 15 },
            new Produto { Nome = "Mouse Gamer", Descricao = "Mouse óptico 6 botões, 3200 DPI.", Preco = 129.50m, QuantidadeEstoque = 30 },
            new Produto { Nome = "Monitor 24\"", Descricao = "Monitor Full HD 75Hz com entrada HDMI.", Preco = 899.00m, QuantidadeEstoque = 8 }
        );
        context.SaveChanges();
    }
}