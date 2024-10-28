using AutoMapper;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Recursos;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Assunto.BuscarPorId;

public class AssuntoBuscaPorIdQueryHandler :
    IRequestHandler<AssuntoBuscaPorIdQuery, Result<AssuntoBuscaPorIdQueryResult>>
{
    private readonly IServicoBuscaAssuntoPorId _servicoBuscaAssuntoPorId;
    private readonly IMapper _mapper;

    public AssuntoBuscaPorIdQueryHandler(IServicoBuscaAssuntoPorId servicoBuscaAssuntoPorId, IMapper mapper)
    {
        _servicoBuscaAssuntoPorId = servicoBuscaAssuntoPorId;
        _mapper = mapper;
    }

    public async Task<Result<AssuntoBuscaPorIdQueryResult>> Handle(AssuntoBuscaPorIdQuery request, CancellationToken cancellationToken)
    {
        Result<AssuntoBuscaPorIdQueryResult> result = new();

        if (request is null)
        {
            result.AddNotification(nameof(AssuntoBuscaPorIdQuery.Id), Mensagens.CodigoAssuntoNaoInformado);

            return await Task.FromResult(result);
        }

        var assuntoEncontrado = await _servicoBuscaAssuntoPorId.BuscarPorIdAsync(request.Id, cancellationToken);

        if (!_servicoBuscaAssuntoPorId.IsValid)
        {
            result.AddNotifications(_servicoBuscaAssuntoPorId.Notifications);

            return await Task.FromResult(result);
        }

        result.Data = _mapper.Map<AssuntoBuscaPorIdQueryResult>(assuntoEncontrado);

        return await Task.FromResult(result);
    }
}
