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
    IRequestHandler<AssuntoCadastroCommand, OneOf<AssuntoCadastroCommandResult, CadastroAssuntoRetorno>>
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

    public async Task<OneOf<AssuntoCadastroCommandResult, CadastroAssuntoRetorno>> Handle(AssuntoCadastroCommand request, CancellationToken cancellationToken)
    {
        var assunto = _mapper.Map<Dominio.Entidades.Assunto>(request);

        if (assunto is null)
        {
            this.AddNotification(nameof(AssuntoCadastroCommand), Mensagens.AssuntoNaoInformado);

            return CadastroAssuntoRetorno.NaoInformado;
        }

        var assuntoCadastradoResult = await _servicoCadastroAssunto.CadastrarAsync(assunto, cancellationToken);

        if (assuntoCadastradoResult.IsT1)
        {
            AddNotifications(_servicoCadastroAssunto.Notifications);

            return assuntoCadastradoResult.AsT1;
        }

        return _mapper.Map<AssuntoCadastroCommandResult>(assuntoCadastradoResult.AsT0);
    }
}