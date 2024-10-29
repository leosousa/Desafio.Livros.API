using Livros.Aplicacao.CasosUso.Autor.BuscarPorId;
using Livros.Aplicacao.CasosUso.Autor.Cadastrar;
using Livros.Aplicacao.CasosUso.Autor.Deletar;
using Livros.Aplicacao.CasosUso.Autor.Editar;
using Livros.Aplicacao.CasosUso.Autor.Listar;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Livros.API.Controllers;

[Route("api/autores")]
public class AutorController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public AutorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cadastra um novo autor
    /// </summary>
    /// <param name="autor">Autor a ser cadastrado</param>
    /// <returns>Id do novo autor cadastrado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(AutorCadastroCommand autor)
    {
        var autorCadastrado = await _mediator.Send(autor);

        if (autorCadastrado is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return autorCadastrado.ResultadoAcao switch
        {
            EResultadoAcaoServico.NaoEncontrado => NotFound(autorCadastrado),
            EResultadoAcaoServico.ParametrosInvalidos => BadRequest(autorCadastrado),
            EResultadoAcaoServico.Erro => StatusCode(StatusCodes.Status500InternalServerError),
            EResultadoAcaoServico.Suceso => CreatedAtAction("BuscarPorId",
                new { id = autorCadastrado.Data.Id },
                autorCadastrado.Data
            ),
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    /// Busca um sautor já cadastrado pelo seu identificador
    /// </summary>
    /// <param name="id">Identificador do autor</param>
    /// <returns>Autor encontrado</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var result = await _mediator.Send(new AutorBuscaPorIdQuery { Id = id });

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
            result.AddNotification("Autor", Mensagens.AutorNaoEncontrado);

            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Busca paginada de autors com filtros informados
    /// </summary>
    /// <param name="nome">Descrição do autors</param>
    /// <param name="numeroPagina">Número da página</param>
    /// <param name="tamanhoPagina">Tamanho da página</param>
    /// <returns>Lista paginada de autors encontrados</returns>
    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery] string? nome,
        [FromQuery] int numeroPagina = 1,
        [FromQuery] int tamanhoPagina = 10)
    {
        var filtros = new AutorListaPaginadaQuery
        {
            Nome = nome,
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
    /// Edita um autor já cadastrado
    /// </summary>
    /// <param name="id" > Id do autor a ser alterado</param>
    /// <param name="autor">Autor a ser alterado</param>
    /// <returns>Autor editado</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] AutorEdicaoCommand autor)
    {
        if (autor is not null) autor.Id = id;

        var autorEditado = await _mediator.Send(autor!);

        if (autorEditado is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return autorEditado.ResultadoAcao switch
        {
            EResultadoAcaoServico.NaoEncontrado => NotFound(autorEditado),
            EResultadoAcaoServico.ParametrosInvalidos => BadRequest(autorEditado),
            EResultadoAcaoServico.Erro => StatusCode(StatusCodes.Status500InternalServerError),
            EResultadoAcaoServico.Suceso => Ok(autorEditado),
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    /// Remove um autor já cadastrado pelo seu identificador
    /// </summary>
    /// <param name="id">Identificador do autor</param>
    /// <returns>Informação se o autor foi ou não removido</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var autorRemovido = await _mediator.Send(new AutorDelecaoCommand { Id = id });

        if (autorRemovido is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return autorRemovido.ResultadoAcao switch
        {
            EResultadoAcaoServico.NaoEncontrado => NotFound(autorRemovido),
            EResultadoAcaoServico.ParametrosInvalidos => BadRequest(autorRemovido),
            EResultadoAcaoServico.Erro => StatusCode(StatusCodes.Status500InternalServerError),
            EResultadoAcaoServico.Suceso => NoContent(),
            _ => throw new NotImplementedException()
        };
    }
}