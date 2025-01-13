using Livros.Dominio.DTOs.Assunto;
using Livros.Dominio.Entidades;
using System.Linq.Expressions;

namespace Livros.Dominio.Contratos;

public interface IRepositorioAssunto : IRepositorio<Assunto>
{
    Task<IEnumerable<AssuntoComLivroDto>> ListarComLivrosAssociadosAsync(
       Expression<Func<Assunto, bool>> predicado,
       int numeroPagina,
       int tamanhoPagina,
       Expression<Func<Assunto, object>>? campoOrdenacao = null,
       bool ordenacaoAscendente = true);
}