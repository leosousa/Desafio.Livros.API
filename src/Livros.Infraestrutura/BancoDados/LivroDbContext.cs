using Livros.Dominio.DTOs;
using Livros.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Livros.Infraestrutura.BancoDados;

public class LivroDbContext : DbContext
{
    public DbSet<Assunto> Assuntos { get; set; }

    public DbSet<Autor> Autores { get; set; }

    public DbSet<Livro> Livros { get; set; }

    public DbSet<RelatorioProducaoLiterariaItem> RelatorioProducaoLiteraria { get; set; }

    public LivroDbContext()
    {
    }

    public LivroDbContext(DbContextOptions<LivroDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}