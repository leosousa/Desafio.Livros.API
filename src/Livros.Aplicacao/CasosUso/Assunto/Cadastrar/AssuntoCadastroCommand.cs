using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Assunto.Cadastrar;

public record AssuntoCadastroCommand : IRequest<Result<AssuntoCadastroCommandResult>>
{
    public string Descricao { get; set; }
}