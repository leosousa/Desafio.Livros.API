using Livros.Dominio.DTOs;
using Livros.Dominio.Servicos.Livro.Listar;

namespace Livros.Dominio.Contratos.Servicos.Livro;

public interface IServicoListagemLivro
{
    Task<ListaPaginadaResult<Entidades.Livro>> ListarAsync(LivroListaFiltro filtros, int numeroPagina = 1, int tamanhoPagina = 10);
}