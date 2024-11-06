using AutoMapper;
using Livros.Aplicacao.Base;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using Livros.Dominio.Servicos.Assunto.Listar;
using Livros.Dominio.Servicos.Autor.Listar;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Livro.Cadastrar;

public sealed class LivroCadastroCommandHandler : ServicoAplicacao,
    IRequestHandler<LivroCadastroCommand, Result<LivroCadastroCommandResult>>
{
    private readonly IMapper _mapper;
    private readonly IServicoCadastroLivro _servicoCadastroLivro;
    private readonly IServicoListagemAutor _servicoListagemAutores;
    private readonly IServicoListagemAssunto _servicoListagemAssuntos;

    public LivroCadastroCommandHandler(
        IMapper mapper,
        IServicoCadastroLivro servicoCadastroLivro,
        IServicoListagemAutor servicoListagemAutores,
        IServicoListagemAssunto servicoListagemAssuntos)
    {
        _mapper = mapper;
        _servicoCadastroLivro = servicoCadastroLivro;
        _servicoListagemAutores = servicoListagemAutores;
        _servicoListagemAssuntos = servicoListagemAssuntos;
    }

    public async Task<Result<LivroCadastroCommandResult>> Handle(LivroCadastroCommand request, CancellationToken cancellationToken)
    {
        Result<LivroCadastroCommandResult> result = new();

        if (request is null)
        {
            result.AddResultadoAcao(EResultadoAcaoServico.ParametrosInvalidos);
            result.AddNotification(nameof(Dominio.Entidades.Livro), Mensagens.LivroNaoInformado);

            return await Task.FromResult(result);
        }

        if (request.Autores is null)
        {
            result.AddResultadoAcao(EResultadoAcaoServico.ParametrosInvalidos);
            result.AddNotification(nameof(Dominio.Entidades.Livro.Autores), Mensagens.AutorNaoInformado);

            return await Task.FromResult(result);
        }

        if (request.Assuntos is null)
        {
            result.AddResultadoAcao(EResultadoAcaoServico.ParametrosInvalidos);
            result.AddNotification(nameof(Dominio.Entidades.Livro.Assuntos), Mensagens.AssuntoNaoInformado);

            return await Task.FromResult(result);
        }

        var autores = await _servicoListagemAutores.ListarAsync(new AutorListaFiltro { Ids = request.Autores });

        if (autores is null || !autores.Itens!.Any())
        {
            result.AddResultadoAcao(EResultadoAcaoServico.NaoEncontrado);
            result.AddNotification(nameof(Dominio.Entidades.Livro.Autores), Mensagens.AutorNaoEncontrado);

            return await Task.FromResult(result);
        }

        var assuntos = await _servicoListagemAssuntos.ListarAsync(new AssuntoListaFiltro { Ids = request.Assuntos });

        if (assuntos is null || !assuntos.Itens!.Any())
        {
            result.AddResultadoAcao(EResultadoAcaoServico.NaoEncontrado);
            result.AddNotification(nameof(Dominio.Entidades.Livro.Assuntos), Mensagens.AssuntoNaoEncontrado);

            return await Task.FromResult(result);
        }

        var livro = _mapper.Map<Dominio.Entidades.Livro>(request);
        livro.AlterarAutores(autores.Itens!.ToList());
        livro.AlterarAssuntos(assuntos.Itens!.ToList());

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