using Livros.Dominio.Contratos;
using Livros.Dominio.Entidades;
using Livros.Infraestrutura.BancoDados;

namespace Livros.Infraestrutura.Repositorios;

public class RepositorioAssunto : Repositorio<Assunto>, IRepositorioAssunto
{
    public RepositorioAssunto(LivroDbContext database) : base(database)
    {
    }
}