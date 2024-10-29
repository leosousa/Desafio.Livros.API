using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Livro.BuscarPorId;

public record LivroBuscaPorIdQuery : IRequest<Result<LivroBuscaPorIdQueryResult>>
{
    public int Id { get; set; }
}
