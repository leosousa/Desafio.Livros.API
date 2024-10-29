using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Entidades;
using Livros.Infraestrutura.BancoDados;

namespace Livros.Infraestrutura.Repositorios;

public class RepositorioAutor : Repositorio<Autor>, IRepositorioAutor
{
    public RepositorioAutor(LivroDbContext database) : base(database)
    {
    }
}