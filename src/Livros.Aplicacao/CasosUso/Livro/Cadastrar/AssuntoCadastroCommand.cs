using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Livro.Cadastrar;

public record LivroCadastroCommand : IRequest<Result<LivroCadastroCommandResult>>
{
    public string Descricao { get; set; }
}