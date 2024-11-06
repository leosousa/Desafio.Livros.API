using Livros.Aplicacao.Base;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Livro.Deletar;

public class LivroDelecaoCommandHandler : ServicoAplicacao,
    IRequestHandler<LivroDelecaoCommand, Result<LivroDelecaoCommandResult>>
{
    private readonly IServicoDelecaoLivro _servicoDelecaoLivro;

    public LivroDelecaoCommandHandler(IServicoDelecaoLivro servicoDelecaoLivro)
    {
        _servicoDelecaoLivro = servicoDelecaoLivro;
    }

    public async Task<Result<LivroDelecaoCommandResult>> Handle(LivroDelecaoCommand request, CancellationToken cancellationToken)
    {
        Result<LivroDelecaoCommandResult> result = new();

        if (request is null)
        {
            result.AddResultadoAcao(EResultadoAcaoServico.ParametrosInvalidos);
            result.AddNotification(nameof(LivroDelecaoCommand.Id), Mensagens.CodigoLivroNaoInformado);

            return await Task.FromResult(result);
        }

        var livroEditado = await _servicoDelecaoLivro.RemoverAsync(request.Id, cancellationToken);

        result.AddResultadoAcao(_servicoDelecaoLivro.ResultadoAcao);
        result.AddNotifications(_servicoDelecaoLivro.Notifications);

        if (_servicoDelecaoLivro.ResultadoAcao != Dominio.Enumeracoes.EResultadoAcaoServico.Suceso)
        {
            return await Task.FromResult(result);
        }

        return await Task.FromResult(result);
    }
}