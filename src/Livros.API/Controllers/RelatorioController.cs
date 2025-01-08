using Livros.Aplicacao.CasosUso.ProducaoLiteraria.RelatorioProducaoLiteraria;
using Livros.Aplicacao.CasosUso.ProducaoLiteraria.RelatorioProducaoLiterariaPdf;
using Livros.Dominio.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Livros.API.Controllers;

[Route("api/relatorios")]
public class RelatorioController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public RelatorioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("producao-literaria/autor/{format}")]
    public async Task<IActionResult> GerarRelatorioProducaoLiteraria(string format)
    {
        var actions = new Dictionary<string, Delegate>
        {
            { "json", new Func<ProducaoLiteraria, Task<IActionResult>>(GerarRelatorioJson) },
            { "pdf", new Func<ProducaoLiteraria, Task<IActionResult>>(GerarRelatorioPdf) }
        };

        var query = new RelatorioProducaoLiterariaQuery();

        var dadosRelatorio = await _mediator.Send(query);

        if (dadosRelatorio is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (dadosRelatorio.Data is null || dadosRelatorio.Data.Relatorio is null)
        {
            return NotFound(dadosRelatorio);
        }

        return await ((Func<ProducaoLiteraria, Task<IActionResult>>)actions[format])(dadosRelatorio!.Data!.Relatorio);
    }

    private async Task<IActionResult> GerarRelatorioPdf(ProducaoLiteraria dadosRelatorio)
    {
        var query = new RelatorioProducaoLiterariaPdfQuery();
        query.Dados = dadosRelatorio;

        var relatorio = await _mediator.Send(query);

        return File(relatorio.PdfFile, "application/pdf", "RelatorioProducaoLiteraria.pdf");
    }

    private async Task<IActionResult> GerarRelatorioJson(ProducaoLiteraria dadosRelatorio)
    {
        return Ok(dadosRelatorio);
    }
}