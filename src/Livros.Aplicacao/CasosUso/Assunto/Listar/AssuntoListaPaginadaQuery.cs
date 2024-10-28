using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Assunto.Listar;

public class AssuntoListaPaginadaQuery : FiltroListaPaginadaQuery, IRequest<AssuntoListaPaginadaQueryResult>
{
    public string? Descricao { get; init; }
}