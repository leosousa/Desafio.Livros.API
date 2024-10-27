using AutoMapper;
using Livros.Aplicacao.Base;
using Livros.Dominio.Contratos;
using Livros.Dominio.Entidades;
using Livros.Dominio.Recursos;
using Livros.Dominio.Servicos.Assunto.Cadastrar;
using MediatR;
using OneOf;

namespace Livros.Aplicacao.CasosUso.Assunto.Cadastrar;

public sealed class AssuntoCadastroCommandHandler : ServicoAplicacao,
    IRequestHandler<AssuntoCadastroCommand, AssuntoCadastroCommandResult>
{
    private readonly IMapper _mapper;
    private readonly IServicoCadastroAssunto _servicoCadastroAssunto;

    public AssuntoCadastroCommandHandler(
        IMapper mapper, 
        IServicoCadastroAssunto servicoCadastroAssunto)
    {
        _mapper = mapper;
        _servicoCadastroAssunto = servicoCadastroAssunto;
    }

    public async Task<AssuntoCadastroCommandResult> Handle(AssuntoCadastroCommand request, CancellationToken cancellationToken)
    {
        var assunto = _mapper.Map<Dominio.Entidades.Assunto>(request);
        AssuntoCadastroCommandResult result = new();

        if (assunto is null)
        {
            result.AddNotification(nameof(AssuntoCadastroCommand), Mensagens.AssuntoNaoInformado);

            return result;
        }

        var assuntoCadastradoResult = await _servicoCadastroAssunto.CadastrarAsync(assunto, cancellationToken);

        if (assuntoCadastradoResult.IsT1)
        {
            result.AddNotifications(_servicoCadastroAssunto.Notifications);

            return result;
        }

        return _mapper.Map<AssuntoCadastroCommandResult>(assuntoCadastradoResult.AsT0);
    }
}