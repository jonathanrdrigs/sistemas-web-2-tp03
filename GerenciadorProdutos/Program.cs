// Aluno: Jonathan Rodrigues Feitosa
// Prontuário: CB3030822

using System.Globalization;
using GerenciadorProdutos.Data;
using GerenciadorProdutos.Models;
using Microsoft.EntityFrameworkCore;

// Cultura fixa: o campo de preço (type="number") envia o valor com ponto.
var cultura = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultura;
CultureInfo.DefaultThreadCurrentUICulture = cultura;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cria/atualiza o banco automaticamente e popula os dados iniciais
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    SeedData.Initialize(context);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Rota personalizada, abre os detalhes do produto
app.MapControllerRoute(
    name: "detalhesProduto",
    pattern: "produto/{id:int}",
    defaults: new { controller = "Produtos", action = "Details" });

// Rota padrão, a página inicial é a lista de produtos
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produtos}/{action=Index}/{id?}");

app.Run();