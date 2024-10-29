using AutoMapper;
using Livros.Aplicacao.Base;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Assunto.Cadastrar;

public sealed class AssuntoCadastroCommandHandler : ServicoAplicacao,
    IRequestHandler<AssuntoCadastroCommand, Result<AssuntoCadastroCommandResult>>
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

    public async Task<Result<AssuntoCadastroCommandResult>> Handle(AssuntoCadastroCommand request, CancellationToken cancellationToken)
    {
        Result<AssuntoCadastroCommandResult> result = new();

        var assunto = _mapper.Map<Dominio.Entidades.Assunto>(request);

        var assuntoCadastrado = await _servicoCadastroAssunto.CadastrarAsync(assunto, cancellationToken);

        result.AddResultadoAcao(_servicoCadastroAssunto.ResultadoAcao);
        result.AddNotifications(_servicoCadastroAssunto.Notifications);

        if (_servicoCadastroAssunto.ResultadoAcao != Dominio.Enumeracoes.EResultadoAcaoServico.Suceso)
        {
            return await Task.FromResult(result);
        }

        result.Data = _mapper.Map<AssuntoCadastroCommandResult>(assuntoCadastrado);

        return await Task.FromResult(result);
    }
}