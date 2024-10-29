using Livros.Aplicacao.CasosUso.Assunto.BuscarPorId;
using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;
using Livros.Aplicacao.CasosUso.Assunto.Deletar;
using Livros.Aplicacao.CasosUso.Assunto.Editar;
using Livros.Aplicacao.CasosUso.Assunto.Listar;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Livros.API.Controllers;

[Route("api/assuntos")]
public class AssuntoController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public AssuntoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cadastra um novo assunto
    /// </summary>
    /// <param name="assunto">Assunto a ser cadastrado</param>
    /// <returns>Id do novo assunto cadastrado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(AssuntoCadastroCommand assunto)
    {
        var assuntoCadastrado = await _mediator.Send(assunto);

        if (assuntoCadastrado is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return assuntoCadastrado.ResultadoAcao switch
        {
            EResultadoAcaoServico.NaoEncontrado => NotFound(assuntoCadastrado),
            EResultadoAcaoServico.ParametrosInvalidos => BadRequest(assuntoCadastrado),
            EResultadoAcaoServico.Erro => StatusCode(StatusCodes.Status500InternalServerError),
            EResultadoAcaoServico.Suceso => CreatedAtAction("BuscarPorId",
                new { id = assuntoCadastrado.Data.Id },
                assuntoCadastrado.Data
            ),
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    /// Busca um sassunto já cadastrado pelo seu identificador
    /// </summary>
    /// <param name="id">Identificador do assunto</param>
    /// <returns>Assunto encontrado</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var result = await _mediator.Send(new AssuntoBuscaPorIdQuery { Id = id });

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
            result.AddNotification("Assunto", Mensagens.AssuntoNaoEncontrado);

            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Busca paginada de assuntos com filtros informados
    /// </summary>
    /// <param name="descricao">Descrição do assuntos</param>
    /// <param name="numeroPagina">Número da página</param>
    /// <param name="tamanhoPagina">Tamanho da página</param>
    /// <returns>Lista paginada de assuntos encontrados</returns>
    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery] string? descricao,
        [FromQuery] int numeroPagina = 1,
        [FromQuery] int tamanhoPagina = 10)
    {
        var filtros = new AssuntoListaPaginadaQuery
        {
            Descricao = descricao,
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
    /// Edita um assunto já cadastrado
    /// </summary>
    /// <param name="id" > Id do assunto a ser alterado</param>
    /// <param name="assunto">Assunto a ser alterado</param>
    /// <returns>Assunto editado</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] AssuntoEdicaoCommand assunto)
    {
        if (assunto is not null) assunto.Id = id;

        var assuntoEditado = await _mediator.Send(assunto!);

        if (assuntoEditado is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return assuntoEditado.ResultadoAcao switch
        {
            EResultadoAcaoServico.NaoEncontrado => NotFound(assuntoEditado),
            EResultadoAcaoServico.ParametrosInvalidos => BadRequest(assuntoEditado),
            EResultadoAcaoServico.Erro => StatusCode(StatusCodes.Status500InternalServerError),
            EResultadoAcaoServico.Suceso => Ok(assuntoEditado),
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    /// Remove um assunto já cadastrado pelo seu identificador
    /// </summary>
    /// <param name="id">Identificador do assunto</param>
    /// <returns>Informação se o assunto foi ou não removido</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var assuntoRemovido = await _mediator.Send(new AssuntoDelecaoCommand { Id = id });

        if (assuntoRemovido is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return assuntoRemovido.ResultadoAcao switch
        {
            EResultadoAcaoServico.NaoEncontrado => NotFound(assuntoRemovido),
            EResultadoAcaoServico.ParametrosInvalidos => BadRequest(assuntoRemovido),
            EResultadoAcaoServico.Erro => StatusCode(StatusCodes.Status500InternalServerError),
            EResultadoAcaoServico.Suceso => NoContent(),
            _ => throw new NotImplementedException()
        };
    }
}