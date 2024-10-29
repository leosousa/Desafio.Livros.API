using AutoMapper;
using Livros.Aplicacao.Base;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos.Servicos.Autor;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Autor.Editar;

public class AutorEdicaoCommandHandler : ServicoAplicacao,
    IRequestHandler<AutorEdicaoCommand, Result<AutorEdicaoCommandResult>>
{
    private readonly IServicoEdicaoAutor _servicoEdicaoAutor;
    private readonly IMapper _mapper;

    public AutorEdicaoCommandHandler(
        IServicoEdicaoAutor servicoEdicaoAutor,
        IMapper mapper)
    {
        _servicoEdicaoAutor = servicoEdicaoAutor;
        _mapper = mapper;
    }

    public async Task<Result<AutorEdicaoCommandResult>> Handle(AutorEdicaoCommand request, CancellationToken cancellationToken)
    {
        Result<AutorEdicaoCommandResult> result = new();

        var autor = _mapper.Map<Dominio.Entidades.Autor>(request);

        var autorEditado = await _servicoEdicaoAutor.EditarAsync(autor, cancellationToken);

        result.AddResultadoAcao(_servicoEdicaoAutor.ResultadoAcao);
        result.AddNotifications(_servicoEdicaoAutor.Notifications);

        if (_servicoEdicaoAutor.ResultadoAcao != Dominio.Enumeracoes.EResultadoAcaoServico.Suceso)
        {
            return await Task.FromResult(result);
        }

        result.Data = _mapper.Map<AutorEdicaoCommandResult>(autorEditado);

        return await Task.FromResult(result);
    }
}