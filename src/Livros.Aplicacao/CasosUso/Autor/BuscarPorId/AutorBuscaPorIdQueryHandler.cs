using AutoMapper;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Recursos;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Autor.BuscarPorId;

public class AutorBuscaPorIdQueryHandler :
    IRequestHandler<AutorBuscaPorIdQuery, Result<AutorBuscaPorIdQueryResult>>
{
    private readonly IServicoBuscaAutorPorId _servicoBuscaAutorPorId;
    private readonly IMapper _mapper;

    public AutorBuscaPorIdQueryHandler(IServicoBuscaAutorPorId servicoBuscaAutorPorId, IMapper mapper)
    {
        _servicoBuscaAutorPorId = servicoBuscaAutorPorId;
        _mapper = mapper;
    }

    public async Task<Result<AutorBuscaPorIdQueryResult>> Handle(AutorBuscaPorIdQuery request, CancellationToken cancellationToken)
    {
        Result<AutorBuscaPorIdQueryResult> result = new();

        if (request is null)
        {
            result.AddNotification(nameof(AutorBuscaPorIdQuery.Id), Mensagens.CodigoAutorNaoInformado);

            return await Task.FromResult(result);
        }

        var autorEncontrado = await _servicoBuscaAutorPorId.BuscarPorIdAsync(request.Id, cancellationToken);

        if (!_servicoBuscaAutorPorId.IsValid)
        {
            result.AddNotifications(_servicoBuscaAutorPorId.Notifications);

            return await Task.FromResult(result);
        }

        result.Data = _mapper.Map<AutorBuscaPorIdQueryResult>(autorEncontrado);

        return await Task.FromResult(result);
    }
}
