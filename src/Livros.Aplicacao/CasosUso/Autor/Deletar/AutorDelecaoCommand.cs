using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Autor.Deletar;

public record AutorDelecaoCommand : IRequest<Result<AutorDelecaoCommandResult>>
{
    public int Id { get; set; }
}