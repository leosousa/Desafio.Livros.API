using AutoMapper;
using Livros.Aplicacao.Base;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Livro.Cadastrar;

public sealed class LivroCadastroCommandHandler : ServicoAplicacao,
    IRequestHandler<LivroCadastroCommand, Result<LivroCadastroCommandResult>>
{
    private readonly IMapper _mapper;
    private readonly IServicoCadastroLivro _servicoCadastroLivro;

    public LivroCadastroCommandHandler(
        IMapper mapper, 
        IServicoCadastroLivro servicoCadastroLivro)
    {
        _mapper = mapper;
        _servicoCadastroLivro = servicoCadastroLivro;
    }

    public async Task<Result<LivroCadastroCommandResult>> Handle(LivroCadastroCommand request, CancellationToken cancellationToken)
    {
        Result<LivroCadastroCommandResult> result = new();

        var livro = _mapper.Map<Dominio.Entidades.Livro>(request);

        var livroCadastrado = await _servicoCadastroLivro.CadastrarAsync(livro, cancellationToken);

        result.AddResultadoAcao(_servicoCadastroLivro.ResultadoAcao);
        result.AddNotifications(_servicoCadastroLivro.Notifications);

        if (_servicoCadastroLivro.ResultadoAcao != Dominio.Enumeracoes.EResultadoAcaoServico.Suceso)
        {
            return await Task.FromResult(result);
        }

        result.Data = _mapper.Map<LivroCadastroCommandResult>(livroCadastrado);

        return await Task.FromResult(result);
    }
}