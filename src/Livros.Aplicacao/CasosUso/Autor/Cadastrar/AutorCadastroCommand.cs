using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Autor.Cadastrar;

public record AutorCadastroCommand : IRequest<Result<AutorCadastroCommandResult>>
{
    public string Nome { get; set; }
}