using Livros.Aplicacao.CasosUso.Livro.BuscarPorId;
using Livros.Aplicacao.CasosUso.Livro.Cadastrar;
using Livros.Aplicacao.CasosUso.Livro.Deletar;
using Livros.Aplicacao.CasosUso.Livro.Editar;
using Livros.Aplicacao.CasosUso.Livro.Listar;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Livros.API.Controllers;

[Route("api/livros")]
public class LivroController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public LivroController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cadastra um novo livro
    /// </summary>
    /// <param name="livro">Livro a ser cadastrado</param>
    /// <returns>Id do novo livro cadastrado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(LivroCadastroCommand livro)
    {
        var livroCadastrado = await _mediator.Send(livro);

        if (livroCadastrado is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return livroCadastrado.ResultadoAcao switch
        {
            EResultadoAcaoServico.NaoEncontrado => NotFound(livroCadastrado),
            EResultadoAcaoServico.ParametrosInvalidos => BadRequest(livroCadastrado),
            EResultadoAcaoServico.Erro => StatusCode(StatusCodes.Status500InternalServerError),
            EResultadoAcaoServico.Suceso => CreatedAtAction("BuscarPorId",
                new { id = livroCadastrado.Data.Id },
                livroCadastrado.Data
            ),
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    /// Busca um slivro já cadastrado pelo seu identificador
    /// </summary>
    /// <param name="id">Identificador do livro</param>
    /// <returns>Livro encontrado</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var result = await _mediator.Send(new LivroBuscaPorIdQuery { Id = id });

        if (result is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (result.Notifications.Any())
        {
            return BadRequest(result);
        }

        if (result.Data is null)
        {
            result.AddNotification("Livro", Mensagens.LivroNaoEncontrado);

            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Busca paginada de livros com filtros informados
    /// </summary>
    /// <param name="titulo">Título do livro</param>
    /// <param name="editora">Editora do livro</param>
    /// <param name="edicao">Edição do livro</param>
    /// <param name="anoPublicacao">Ano de publicação do livro</param>
    /// <param name="numeroPagina">Número da página</param>
    /// <param name="tamanhoPagina">Tamanho da página</param>
    /// <returns>Lista paginada de livros encontrados</returns>
    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery] string? titulo,
        [FromQuery] string? editora,
        [FromQuery] int? edicao,
        [FromQuery] int? anoPublicacao,
        [FromQuery] string? assunto,
        [FromQuery] string? autor,
        [FromQuery] int numeroPagina = 1,
        [FromQuery] int tamanhoPagina = 10)
    {
        var filtros = new LivroListaPaginadaQuery
        {
            Titulo = titulo,
            Editora = editora,
            Edicao = edicao,
            AnoPublicacao = anoPublicacao,
            Assunto = assunto,
            Autor = autor,
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina
        };

        var result = await _mediator.Send(filtros);

        if (result is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (!result.Itens!.Any())
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Edita um livro já cadastrado
    /// </summary>
    /// <param name="id" > Id do livro a ser alterado</param>
    /// <param name="livro">Livro a ser alterado</param>
    /// <returns>Livro editado</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] LivroEdicaoCommand livro)
    {
        if (livro is not null) livro.Id = id;

        var livroEditado = await _mediator.Send(livro!);

        if (livroEditado is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return livroEditado.ResultadoAcao switch
        {
            EResultadoAcaoServico.NaoEncontrado => NotFound(livroEditado),
            EResultadoAcaoServico.ParametrosInvalidos => BadRequest(livroEditado),
            EResultadoAcaoServico.Erro => StatusCode(StatusCodes.Status500InternalServerError),
            EResultadoAcaoServico.Suceso => Ok(livroEditado),
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    /// Remove um livro já cadastrado pelo seu identificador
    /// </summary>
    /// <param name="id">Identificador do livro</param>
    /// <returns>Informação se o livro foi ou não removido</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var livroRemovido = await _mediator.Send(new LivroDelecaoCommand { Id = id });

        if (livroRemovido is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return livroRemovido.ResultadoAcao switch
        {
            EResultadoAcaoServico.NaoEncontrado => NotFound(livroRemovido),
            EResultadoAcaoServico.ParametrosInvalidos => BadRequest(livroRemovido),
            EResultadoAcaoServico.Erro => StatusCode(StatusCodes.Status500InternalServerError),
            EResultadoAcaoServico.Suceso => NoContent(),
            _ => throw new NotImplementedException()
        };
    }
}