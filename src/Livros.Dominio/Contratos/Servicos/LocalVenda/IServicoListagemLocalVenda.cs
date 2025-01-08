using Livros.Dominio.DTOs;
using Livros.Dominio.Servicos.LocalVenda.Listar;

namespace Livros.Dominio.Contratos.Servicos.LocalVenda;

public interface IServicoListagemLocalVenda
{
    Task<ListaPaginadaResult<Entidades.LocalVenda>> ListarAsync(LocalVendaListaFiltro filtros, int numeroPagina = 1, int tamanhoPagina = 10);
}