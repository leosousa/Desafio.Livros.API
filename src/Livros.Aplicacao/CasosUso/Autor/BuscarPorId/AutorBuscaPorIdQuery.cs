using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Autor.BuscarPorId;

public record AutorBuscaPorIdQuery : IRequest<Result<AutorBuscaPorIdQueryResult>>
{
    public int Id { get; set; }
}
