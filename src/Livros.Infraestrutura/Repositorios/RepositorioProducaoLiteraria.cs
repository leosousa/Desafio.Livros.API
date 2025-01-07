using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.DTOs;
using Livros.Infraestrutura.BancoDados;
using Microsoft.EntityFrameworkCore;

namespace Livros.Infraestrutura.Repositorios;

public class RepositorioProducaoLiteraria : IRepositorioProducaoLiteraria
{
    protected LivroDbContext _database;

    public RepositorioProducaoLiteraria(LivroDbContext database)
    {
        _database = database;
    }

    public async Task<IEnumerable<RelatorioProducaoLiterariaItem>> ListarProducaoLiteraria()
    {
        return await _database.RelatorioProducaoLiteraria.ToListAsync();
    }
}