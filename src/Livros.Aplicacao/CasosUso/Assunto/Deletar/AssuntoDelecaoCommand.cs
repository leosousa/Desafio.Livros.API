using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Assunto.Deletar;

public record AssuntoDelecaoCommand : IRequest<Result<AssuntoDelecaoCommandResult>>
{
    public int Id { get; set; }
}