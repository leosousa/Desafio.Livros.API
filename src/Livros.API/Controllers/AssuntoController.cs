using Livros.Aplicacao.CasosUso.Assunto.BuscarPorId;
using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;
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
}