using MediatR;

namespace Livros.Aplicacao.CasosUso.Assunto.Cadastrar;

public record AssuntoCadastroCommand : IRequest<AssuntoCadastroCommandResult>
{
    public string Descricao { get; set; }
}