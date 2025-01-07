using Livros.Aplicacao.CasosUso.ProducaoLiteraria.RelatorioProducaoLiteraria;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Livros.API.Controllers;

[Route("api/relatorios")]
public class RelatorioController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public RelatorioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("producao-literaria/autor")]
    public async Task<IActionResult> GerarRelatorioProducaoLiteraria()
    {
        var query = new RelatorioProducaoLiterariaQuery();

        var result = await _mediator.Send(query);

        if (result is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (result.Data is null || result.Data.Relatorio is null)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}