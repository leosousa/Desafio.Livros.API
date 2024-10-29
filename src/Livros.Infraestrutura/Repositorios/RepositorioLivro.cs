using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Entidades;
using Livros.Infraestrutura.BancoDados;

namespace Livros.Infraestrutura.Repositorios;

public class RepositorioLivro : Repositorio<Livro>, IRepositorioLivro
{
    public RepositorioLivro(LivroDbContext database) : base(database)
    {
    }
}