using Livros.Dominio.Servicos.Assunto.Cadastrar;
using MediatR;
using OneOf;

namespace Livros.Aplicacao.CasosUso.Assunto.Cadastrar;

public record AssuntoCadastroCommand : IRequest<OneOf<AssuntoCadastroCommandResult, CadastroAssuntoRetorno>>
{
    public string Descricao { get; set; }
}