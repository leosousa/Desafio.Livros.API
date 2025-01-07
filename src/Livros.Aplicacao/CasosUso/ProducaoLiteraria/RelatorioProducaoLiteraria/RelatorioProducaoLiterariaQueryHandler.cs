using AutoMapper;
using Livros.Aplicacao.Base;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos.Servicos.ProducaoLiteraria;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using MediatR;

namespace Livros.Aplicacao.CasosUso.ProducaoLiteraria.RelatorioProducaoLiteraria;

public class RelatorioProducaoLiterariaQueryHandler : ServicoAplicacao,
    IRequestHandler<RelatorioProducaoLiterariaQuery, Result<RelatorioProducaoLiterariaQueryResult>>
{
    private readonly IServicoRelatorioProducaoLiteraria _servicoRelatorio;
    private readonly IMapper _mapper;

    public RelatorioProducaoLiterariaQueryHandler(IServicoRelatorioProducaoLiteraria servicoRelatorio, IMapper mapper)
    {
        _servicoRelatorio = servicoRelatorio;
        _mapper = mapper;
    }

    public async Task<Result<RelatorioProducaoLiterariaQueryResult>> Handle(RelatorioProducaoLiterariaQuery request, CancellationToken cancellationToken)
    {
        Result<RelatorioProducaoLiterariaQueryResult> result = new();

        var dadosRelatorio = await _servicoRelatorio.ListarAsync();

        if (dadosRelatorio is null || !dadosRelatorio.Itens!.Any())
        {
            result.AddResultadoAcao(EResultadoAcaoServico.NaoEncontrado);
            result.AddNotification(nameof(Dominio.ValueObjects.ProducaoLiteraria), Mensagens.RelatorioProducaoLiterariaNaoGerado);

            return await Task.FromResult(result);
        }

        result.Data = _mapper.Map<RelatorioProducaoLiterariaQueryResult>(dadosRelatorio);

        return await Task.FromResult(result);
    }
}