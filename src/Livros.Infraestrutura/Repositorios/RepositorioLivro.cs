using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Entidades;
using Livros.Infraestrutura.BancoDados;
using Microsoft.EntityFrameworkCore;

namespace Livros.Infraestrutura.Repositorios;

public class RepositorioLivro : Repositorio<Livro>, IRepositorioLivro
{
    public RepositorioLivro(LivroDbContext database) : base(database)
    {
    }

    public override async Task<Livro?> BuscarPorIdAsync(int id)
    {
        return await _database.Livros
            .Include(livroAssunto => livroAssunto.Assuntos)
            .Include(livroAutores => livroAutores.Autores)
            .FirstOrDefaultAsync(entity => entity.Id == id);
    }
}