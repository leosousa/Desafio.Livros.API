using Livros.Aplicacao.CasosUso.Assunto.BuscarPorId;
using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;
using Livros.Aplicacao.CasosUso.Assunto.Editar;
using Livros.Aplicacao.CasosUso.Assunto.Listar;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Livros.API.Controllers;

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
    public async Task<IActionResult> Create(AssuntoCadastroCommand assunto)
    {
        var registered = await _mediator.Send(assunto);

        if (registered is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (registered.Notifications.Any())
        {
            return BadRequest(registered.Notifications);
        }

        return CreatedAtAction("GetById",
           new
           {
               id = registered.Id
           },
           registered
        );
    }

    /// <summary>
    /// Busca um sassunto já cadastrado pelo seu identificador
    /// </summary>
    /// <param name="id">Identificador do assunto</param>
    /// <returns>Assunto encontrado</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
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
    /// <param name="produto">Assunto a ser alterado</param>
    /// <returns>Assunto editado</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] AssuntoEdicaoCommand produto)
    {
        if (produto is not null) produto.Id = id;

        var assuntoEditado = await _mediator.Send(produto!);

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
}