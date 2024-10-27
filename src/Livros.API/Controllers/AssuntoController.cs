using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;
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
}