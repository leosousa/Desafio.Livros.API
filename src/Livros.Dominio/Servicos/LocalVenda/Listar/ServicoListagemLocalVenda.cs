using Flunt.Notifications;
using LinqKit;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Contratos.Servicos.LocalVenda;
using Livros.Dominio.DTOs;
using Livros.Dominio.Servicos.Livro.Listar;

namespace Livros.Dominio.Servicos.LocalVenda.Listar;

public class ServicoListagemLocalVenda : Notifiable<Notification>, IServicoListagemLocalVenda
{
    private readonly IRepositorioLocalVenda _repositorioLocalVenda;

    public ServicoListagemLocalVenda(IRepositorioLocalVenda repositorioLocalVenda)
    {
        _repositorioLocalVenda = repositorioLocalVenda;
    }

    public async Task<ListaPaginadaResult<Entidades.LocalVenda>> ListarAsync(LocalVendaListaFiltro filtros, int numeroPagina = 1, int tamanhoPagina = 10)
    {
        var predicado = PredicateBuilder.New<Entidades.LocalVenda>(true);

        if (filtros is not null)
        {
            predicado = AdicionarFiltrosBuscaNaConsulta(filtros, predicado);
        }

        var quantidadeProdutos = await _repositorioLocalVenda.CountAsync(predicado);

        // O conceito de paginação para o negócio vai da página 1 até a página N
        if (numeroPagina <= 0)
        {
            // Com isso, a página informada menor ou igual a 0 setamos para 1
            numeroPagina = 1;
        }

        // Ao enviar para o repositório, trabalhamos com o conceito de página 0 até N,
        // para sabermos quantos registros pular internamente na busca, por isso enviamos
        // numeroPagina - 1
        var livros = await _repositorioLocalVenda.ListarAsync(predicado, numeroPagina - 1, tamanhoPagina);

        var totalPaginas = quantidadeProdutos / tamanhoPagina;

        var result = new ListaPaginadaResult<Entidades.LocalVenda>
        {
            Itens = livros,
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            TotalRegistros = quantidadeProdutos,
            TotalPaginas = (quantidadeProdutos % tamanhoPagina == 0) ? totalPaginas : totalPaginas + 1
        };

        return await Task.FromResult(result);
    }

    private static ExpressionStarter<Entidades.LocalVenda> AdicionarFiltrosBuscaNaConsulta(LocalVendaListaFiltro filtros, ExpressionStarter<Entidades.LocalVenda> predicado)
    {
        if (!string.IsNullOrEmpty(filtros.Descricao))
        {
            predicado = predicado.And(p => p.Descricao.Contains(filtros.Descricao));
        }

        return predicado;
    }
}
