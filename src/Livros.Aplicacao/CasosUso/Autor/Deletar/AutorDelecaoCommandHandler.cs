using Livros.Aplicacao.Base;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Autor.Deletar;

public class AutorDelecaoCommandHandler : ServicoAplicacao,
    IRequestHandler<AutorDelecaoCommand, Result<AutorDelecaoCommandResult>>
{
    private readonly IServicoDelecaoAutor _servicoDelecaoAutor;

    public AutorDelecaoCommandHandler(IServicoDelecaoAutor servicoDelecaoAutor)
    {
        _servicoDelecaoAutor = servicoDelecaoAutor;
    }

    public async Task<Result<AutorDelecaoCommandResult>> Handle(AutorDelecaoCommand request, CancellationToken cancellationToken)
    {
        Result<AutorDelecaoCommandResult> result = new();

        if (request is null)
        {
            result.AddResultadoAcao(EResultadoAcaoServico.ParametrosInvalidos);
            result.AddNotification(nameof(AutorDelecaoCommand.Id), Mensagens.CodigoAutorNaoInformado);

            return await Task.FromResult(result);
        }

        var autorEditado = await _servicoDelecaoAutor.RemoverAsync(request.Id, cancellationToken);

        result.AddResultadoAcao(_servicoDelecaoAutor.ResultadoAcao);
        result.AddNotifications(_servicoDelecaoAutor.Notifications);

        if (_servicoDelecaoAutor.ResultadoAcao != Dominio.Enumeracoes.EResultadoAcaoServico.Suceso)
        {
            return await Task.FromResult(result);
        }

        return await Task.FromResult(result);
    }
}