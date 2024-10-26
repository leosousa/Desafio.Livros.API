using MediatR;

namespace Livros.Aplicacao.CasosUso.Assunto.Cadastrar;

public sealed class AssuntoCadastroCommandHandler :
    IRequestHandler<AssuntoCadastroCommand, AssuntoCadastroCommandResult?>
{
    public async Task<AssuntoCadastroCommandResult?> Handle(AssuntoCadastroCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}