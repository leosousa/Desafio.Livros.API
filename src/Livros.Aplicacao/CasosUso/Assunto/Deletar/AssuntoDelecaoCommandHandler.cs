using Livros.Aplicacao.Base;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Assunto.Deletar;

public class AssuntoDelecaoCommandHandler : ServicoAplicacao,
    IRequestHandler<AssuntoDelecaoCommand, Result<AssuntoDelecaoCommandResult>>
{
    private readonly IServicoDelecaoAssunto _servicoDelecaoAssunto;

    public AssuntoDelecaoCommandHandler(IServicoDelecaoAssunto servicoDelecaoAssunto)
    {
        _servicoDelecaoAssunto = servicoDelecaoAssunto;
    }

    public async Task<Result<AssuntoDelecaoCommandResult>> Handle(AssuntoDelecaoCommand request, CancellationToken cancellationToken)
    {
        Result<AssuntoDelecaoCommandResult> result = new();

        if (request is null)
        {
            result.AddResultadoAcao(EResultadoAcaoServico.ParametrosInvalidos);
            result.AddNotification(nameof(AssuntoDelecaoCommand.Id), Mensagens.CodigoAssuntoNaoInformado);

            return await Task.FromResult(result);
        }

        var assuntoEditado = await _servicoDelecaoAssunto.RemoverAsync(request.Id, cancellationToken);

        result.AddResultadoAcao(_servicoDelecaoAssunto.ResultadoAcao);
        result.AddNotifications(_servicoDelecaoAssunto.Notifications);

        if (_servicoDelecaoAssunto.ResultadoAcao != Dominio.Enumeracoes.EResultadoAcaoServico.Suceso)
        {
            return await Task.FromResult(result);
        }

        return await Task.FromResult(result);
    }
}