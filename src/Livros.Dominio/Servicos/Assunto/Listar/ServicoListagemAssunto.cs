using Flunt.Notifications;
using LinqKit;
using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.DTOs;

namespace Livros.Dominio.Servicos.Assunto.Listar;

public class ServicoListagemAssunto : Notifiable<Notification>, IServicoListagemAssunto
{
    private readonly IRepositorioAssunto _repositorioAssunto;

    public ServicoListagemAssunto(IRepositorioAssunto repositorioAssunto)
    {
        _repositorioAssunto = repositorioAssunto;
    }

    public async Task<ListaPaginadaResult<Entidades.Assunto>> ListarAsync(AssuntoListaFiltro filtros, int numeroPagina = 1, int tamanhoPagina = 10)
    {
        var predicado = PredicateBuilder.New<Entidades.Assunto>(true);

        if (filtros is not null)
        {
            predicado = AdicionarFiltrosBuscaNaConsulta(filtros, predicado);
        }

        var quantidadeProdutos = await _repositorioAssunto.CountAsync(predicado);

        // O conceito de paginação para o negócio vai da página 1 até a página N
        if (numeroPagina <= 0)
        {
            // Com isso, a página informada menor ou igual a 0 setamos para 1
            numeroPagina = 1;
        }

        // Ao enviar para o repositório, trabalhamos com o conceito de página 0 até N,
        // para sabermos quantos registros pular internamente na busca, por isso enviamos
        // numeroPagina - 1
        var assuntos = await _repositorioAssunto.ListarAsync(predicado, numeroPagina - 1, tamanhoPagina, e => e.Descricao);

        var totalPaginas = quantidadeProdutos / tamanhoPagina;

        var result = new ListaPaginadaResult<Entidades.Assunto>
        {
            Itens = assuntos,
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            TotalRegistros = quantidadeProdutos,
            TotalPaginas = (quantidadeProdutos % tamanhoPagina == 0) ? totalPaginas : totalPaginas + 1
        };

        return await Task.FromResult(result);
    }

    private static ExpressionStarter<Entidades.Assunto> AdicionarFiltrosBuscaNaConsulta(AssuntoListaFiltro filtros, ExpressionStarter<Entidades.Assunto> predicado)
    {
        if (filtros.Ids is not null)
        {
            predicado = predicado.And(p => filtros.Ids.Contains(p.Id));
        }

        if (!string.IsNullOrEmpty(filtros.Descricao))
        {
            predicado = predicado.And(p => p.Descricao.Contains(filtros.Descricao));
        }

        return predicado;
    }
}