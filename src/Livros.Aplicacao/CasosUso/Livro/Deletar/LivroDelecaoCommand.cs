using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Livro.Deletar;

public record LivroDelecaoCommand : IRequest<Result<LivroDelecaoCommandResult>>
{
    public int Id { get; set; }
}