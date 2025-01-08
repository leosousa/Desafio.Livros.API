using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Entidades;
using Livros.Infraestrutura.BancoDados;

namespace Livros.Infraestrutura.Repositorios;

public class RepositorioLocalVenda : Repositorio<LocalVenda>, IRepositorioLocalVenda
{
    public RepositorioLocalVenda(LivroDbContext database) : base(database)
    {
    }
}