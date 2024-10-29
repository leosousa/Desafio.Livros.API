using AutoMapper;
using Livros.Aplicacao.Base;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos.Servicos.Livro;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Livro.Editar;

public class LivroEdicaoCommandHandler : ServicoAplicacao,
    IRequestHandler<LivroEdicaoCommand, Result<LivroEdicaoCommandResult>>
{
    private readonly IServicoEdicaoLivro _servicoEdicaoLivro;
    private readonly IMapper _mapper;

    public LivroEdicaoCommandHandler(
        IServicoEdicaoLivro servicoEdicaoLivro,
        IMapper mapper)
    {
        _servicoEdicaoLivro = servicoEdicaoLivro;
        _mapper = mapper;
    }

    public async Task<Result<LivroEdicaoCommandResult>> Handle(LivroEdicaoCommand request, CancellationToken cancellationToken)
    {
        Result<LivroEdicaoCommandResult> result = new();

        var livro = _mapper.Map<Dominio.Entidades.Livro>(request);

        var livroEditado = await _servicoEdicaoLivro.EditarAsync(livro, cancellationToken);

        result.AddResultadoAcao(_servicoEdicaoLivro.ResultadoAcao);
        result.AddNotifications(_servicoEdicaoLivro.Notifications);

        if (_servicoEdicaoLivro.ResultadoAcao != Dominio.Enumeracoes.EResultadoAcaoServico.Suceso)
        {
            return await Task.FromResult(result);
        }

        result.Data = _mapper.Map<LivroEdicaoCommandResult>(livroEditado);

        return await Task.FromResult(result);
    }
}