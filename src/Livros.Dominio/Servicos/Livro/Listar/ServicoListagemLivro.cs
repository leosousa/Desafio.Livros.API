using Flunt.Notifications;
using LinqKit;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.DTOs;
using System.Linq;

namespace Livros.Dominio.Servicos.Livro.Listar;

public class ServicoListagemLivro : Notifiable<Notification>, IServicoListagemLivro
{
    private readonly IRepositorioLivro _repositorioLivro;

    public ServicoListagemLivro(IRepositorioLivro repositorioLivro)
    {
        _repositorioLivro = repositorioLivro;
    }

    public async Task<ListaPaginadaResult<Entidades.Livro>> ListarAsync(LivroListaFiltro filtros, int numeroPagina = 1, int tamanhoPagina = 10)
    {
        var predicado = PredicateBuilder.New<Entidades.Livro>(true);

        if (filtros is not null)
        {
            predicado = AdicionarFiltrosBuscaNaConsulta(filtros, predicado);
        }

        var quantidadeProdutos = await _repositorioLivro.CountAsync(predicado);

        // O conceito de paginação para o negócio vai da página 1 até a página N
        if (numeroPagina <= 0)
        {
            // Com isso, a página informada menor ou igual a 0 setamos para 1
            numeroPagina = 1;
        }

        // Ao enviar para o repositório, trabalhamos com o conceito de página 0 até N,
        // para sabermos quantos registros pular internamente na busca, por isso enviamos
        // numeroPagina - 1
        var livros = await _repositorioLivro.ListarAsync(predicado, numeroPagina - 1, tamanhoPagina);

        var totalPaginas = quantidadeProdutos / tamanhoPagina;

        var result = new ListaPaginadaResult<Entidades.Livro>
        {
            Itens = livros,
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            TotalRegistros = quantidadeProdutos,
            TotalPaginas = (quantidadeProdutos % tamanhoPagina == 0) ? totalPaginas : totalPaginas + 1
        };

        return await Task.FromResult(result);
    }

    private static ExpressionStarter<Entidades.Livro> AdicionarFiltrosBuscaNaConsulta(LivroListaFiltro filtros, ExpressionStarter<Entidades.Livro> predicado)
    {
        if (!string.IsNullOrEmpty(filtros.Titulo))
        {
            predicado = predicado.And(p => p.Titulo.Contains(filtros.Titulo));
        }

        if (!string.IsNullOrEmpty(filtros.Editora))
        {
            predicado = predicado.And(p => p.Editora.Contains(filtros.Editora));
        }

        if (filtros.Edicao is not null)
        {
            predicado = predicado.And(p => p.Edicao == filtros.Edicao!);
        }

        if (filtros.AnoPublicacao is not null)
        {
            predicado = predicado.And(p => p.AnoPublicacao == filtros.AnoPublicacao!);
        }

        if (!string.IsNullOrEmpty(filtros.Autor))
        {
            predicado = predicado.And(p => p.Autores.Any(autor => autor.Nome.Contains(filtros.Autor!)));
        }

        if (!string.IsNullOrEmpty(filtros.Assunto))
        {
            predicado = predicado.And(p => p.Assuntos.Any(assunto => assunto.Descricao.Contains(filtros.Assunto!)));
        }

        return predicado;
    }
}