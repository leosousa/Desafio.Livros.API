using Livros.Dominio.DTOs;
using Livros.Dominio.DTOs.Assunto;
using Livros.Dominio.Servicos.Assunto.Listar;

namespace Livros.Dominio.Contratos.Servicos.Assunto;

public interface IServicoListagemAssunto
{
    Task<ListaPaginadaResult<Entidades.Assunto>> ListarAsync(AssuntoListaFiltro filtros, int numeroPagina = 1, int tamanhoPagina = 10);
    Task<ListaPaginadaResult<AssuntoComLivroDto>> ListarComLivrosAsync(AssuntoListaFiltro filtros, int numeroPagina = 1, int tamanhoPagina = 10);
}