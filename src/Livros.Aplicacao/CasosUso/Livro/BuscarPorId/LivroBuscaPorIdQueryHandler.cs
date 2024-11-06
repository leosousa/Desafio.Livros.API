using AutoMapper;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.Recursos;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Livro.BuscarPorId;

public class LivroBuscaPorIdQueryHandler :
    IRequestHandler<LivroBuscaPorIdQuery, Result<LivroBuscaPorIdQueryResult>>
{
    private readonly IServicoBuscaLivroPorId _servicoBuscaLivroPorId;
    private readonly IMapper _mapper;

    public LivroBuscaPorIdQueryHandler(IServicoBuscaLivroPorId servicoBuscaLivroPorId, IMapper mapper)
    {
        _servicoBuscaLivroPorId = servicoBuscaLivroPorId;
        _mapper = mapper;
    }

    public async Task<Result<LivroBuscaPorIdQueryResult>> Handle(LivroBuscaPorIdQuery request, CancellationToken cancellationToken)
    {
        Result<LivroBuscaPorIdQueryResult> result = new();

        if (request is null)
        {
            result.AddNotification(nameof(LivroBuscaPorIdQuery.Id), Mensagens.CodigoLivroNaoInformado);

            return await Task.FromResult(result);
        }

        var livroEncontrado = await _servicoBuscaLivroPorId.BuscarPorIdAsync(request.Id, cancellationToken);

        if (!_servicoBuscaLivroPorId.IsValid)
        {
            result.AddNotifications(_servicoBuscaLivroPorId.Notifications);

            return await Task.FromResult(result);
        }

        result.Data = _mapper.Map<LivroBuscaPorIdQueryResult>(livroEncontrado);

        return await Task.FromResult(result);
    }
}
