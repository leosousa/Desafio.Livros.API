using Livros.Dominio.DTOs.Autor;
using Livros.Dominio.Entidades;
using System.Linq.Expressions;

namespace Livros.Dominio.Contratos.Repositorios;

public interface IRepositorioAutor : IRepositorio<Autor>
{
    Task<IEnumerable<AutorComLivroDto>> ListarComLivrosAssociadosAsync(
        Expression<Func<Autor, bool>> predicado,
        int numeroPagina,
        int tamanhoPagina,
        Expression<Func<Autor, object>>? campoOrdenacao = null,
        bool ordenacaoAscendente = true);
}