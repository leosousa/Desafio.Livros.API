using AutoMapper;
using Livros.Aplicacao.Base;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos.Servicos.Assunto;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Assunto.Editar;

public class AssuntoEdicaoCommandHandler : ServicoAplicacao,
    IRequestHandler<AssuntoEdicaoCommand, Result<AssuntoEdicaoCommandResult>>
{
    private readonly IServicoEdicaoAssunto _servicoEdicaoAssunto;
    private readonly IMapper _mapper;

    public AssuntoEdicaoCommandHandler(
        IServicoEdicaoAssunto servicoEdicaoAssunto,
        IMapper mapper)
    {
        _servicoEdicaoAssunto = servicoEdicaoAssunto;
        _mapper = mapper;
    }

    public async Task<Result<AssuntoEdicaoCommandResult>> Handle(AssuntoEdicaoCommand request, CancellationToken cancellationToken)
    {
        Result<AssuntoEdicaoCommandResult> result = new();

        var assunto = _mapper.Map<Dominio.Entidades.Assunto>(request);

        var assuntoEditado = await _servicoEdicaoAssunto.EditarAsync(assunto, cancellationToken);

        result.AddResultadoAcao(_servicoEdicaoAssunto.ResultadoAcao);
        result.AddNotifications(_servicoEdicaoAssunto.Notifications);

        if (_servicoEdicaoAssunto.ResultadoAcao != Dominio.Enumeracoes.EResultadoAcaoServico.Suceso)
        {
            return await Task.FromResult(result);
        }

        result.Data = _mapper.Map<AssuntoEdicaoCommandResult>(assuntoEditado);

        return await Task.FromResult(result);
    }
}