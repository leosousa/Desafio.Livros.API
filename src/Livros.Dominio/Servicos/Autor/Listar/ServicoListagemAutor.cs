using Flunt.Notifications;
using LinqKit;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.DTOs;

namespace Livros.Dominio.Servicos.Autor.Listar;

public class ServicoListagemAutor : Notifiable<Notification>, IServicoListagemAutor
{
    private readonly IRepositorioAutor _repositorioAutor;

    public ServicoListagemAutor(IRepositorioAutor repositorioAutor)
    {
        _repositorioAutor = repositorioAutor;
    }

    public async Task<ListaPaginadaResult<Entidades.Autor>> ListarAsync(AutorListaFiltro filtros, int numeroPagina = 1, int tamanhoPagina = 10)
    {
        var predicado = PredicateBuilder.New<Entidades.Autor>(true);

        if (filtros is not null)
        {
            predicado = AdicionarFiltrosBuscaNaConsulta(filtros, predicado);
        }

        var quantidadeProdutos = await _repositorioAutor.CountAsync(predicado);

        // O conceito de paginação para o negócio vai da página 1 até a página N
        if (numeroPagina <= 0)
        {
            // Com isso, a página informada menor ou igual a 0 setamos para 1
            numeroPagina = 1;
        }

        // Ao enviar para o repositório, trabalhamos com o conceito de página 0 até N,
        // para sabermos quantos registros pular internamente na busca, por isso enviamos
        // numeroPagina - 1
        var autors = await _repositorioAutor.ListarAsync(predicado, numeroPagina - 1, tamanhoPagina);

        var totalPaginas = quantidadeProdutos / tamanhoPagina;

        var result = new ListaPaginadaResult<Entidades.Autor>
        {
            Itens = autors,
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            TotalRegistros = quantidadeProdutos,
            TotalPaginas = (quantidadeProdutos % tamanhoPagina == 0) ? totalPaginas : totalPaginas + 1
        };

        return await Task.FromResult(result);
    }

    private static ExpressionStarter<Entidades.Autor> AdicionarFiltrosBuscaNaConsulta(AutorListaFiltro filtros, ExpressionStarter<Entidades.Autor> predicado)
    {
        if (filtros.Ids is not null)
        {
            predicado = predicado.And(p => filtros.Ids.Contains(p.Id));
        }

        if (!string.IsNullOrEmpty(filtros.Nome))
        {
            predicado = predicado.And(p => p.Nome.Contains(filtros.Nome));
        }

        return predicado;
    }
}