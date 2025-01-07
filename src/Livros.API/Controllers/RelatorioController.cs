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
        return null;
    }
}