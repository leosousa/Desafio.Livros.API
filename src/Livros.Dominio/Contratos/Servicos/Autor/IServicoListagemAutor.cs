using Livros.Dominio.DTOs;
using Livros.Dominio.Servicos.Autor.Listar;

namespace Livros.Dominio.Contratos.Servicos.Autor;

public interface IServicoListagemAutor
{
    Task<ListaPaginadaResult<Entidades.Autor>> ListarAsync(AutorListaFiltro filtros, int numeroPagina = 1, int tamanhoPagina = 10);
}