using AutoMapper;
using Livros.Aplicacao.Base;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Autor.Cadastrar;

public sealed class AutorCadastroCommandHandler : ServicoAplicacao,
    IRequestHandler<AutorCadastroCommand, Result<AutorCadastroCommandResult>>
{
    private readonly IMapper _mapper;
    private readonly IServicoCadastroAutor _servicoCadastroAutor;

    public AutorCadastroCommandHandler(
        IMapper mapper, 
        IServicoCadastroAutor servicoCadastroAutor)
    {
        _mapper = mapper;
        _servicoCadastroAutor = servicoCadastroAutor;
    }

    public async Task<Result<AutorCadastroCommandResult>> Handle(AutorCadastroCommand request, CancellationToken cancellationToken)
    {
        Result<AutorCadastroCommandResult> result = new();

        var autor = _mapper.Map<Dominio.Entidades.Autor>(request);

        var autorCadastrado = await _servicoCadastroAutor.CadastrarAsync(autor, cancellationToken);

        result.AddResultadoAcao(_servicoCadastroAutor.ResultadoAcao);
        result.AddNotifications(_servicoCadastroAutor.Notifications);

        if (_servicoCadastroAutor.ResultadoAcao != Dominio.Enumeracoes.EResultadoAcaoServico.Suceso)
        {
            return await Task.FromResult(result);
        }

        result.Data = _mapper.Map<AutorCadastroCommandResult>(autorCadastrado);

        return await Task.FromResult(result);
    }
}