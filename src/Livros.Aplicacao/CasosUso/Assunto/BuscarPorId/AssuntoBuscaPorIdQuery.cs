using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Assunto.BuscarPorId;

public record AssuntoBuscaPorIdQuery : IRequest<Result<AssuntoBuscaPorIdQueryResult>>
{
    public int Id { get; set; }
}
