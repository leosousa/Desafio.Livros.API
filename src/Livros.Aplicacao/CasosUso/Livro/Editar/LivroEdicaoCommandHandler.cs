using AutoMapper;
using Livros.Aplicacao.Base;
using Livros.Aplicacao.DTOs;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.Contratos.Servicos.LocalVenda;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using Livros.Dominio.Servicos.Assunto.Listar;
using Livros.Dominio.Servicos.Autor.Listar;
using Livros.Dominio.Servicos.LocalVenda.Listar;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Livros.Aplicacao.CasosUso.Livro.Editar;

public class LivroEdicaoCommandHandler : ServicoAplicacao,
    IRequestHandler<LivroEdicaoCommand, Result<LivroEdicaoCommandResult>>
{
    private readonly IMapper _mapper;
    private readonly IServicoEdicaoLivro _servicoEdicaoLivro;
    private readonly IServicoListagemAutor _servicoListagemAutores;
    private readonly IServicoListagemAssunto _servicoListagemAssuntos;
    private readonly IServicoListagemLocalVenda _servicoListagemLocaisVenda;

    public LivroEdicaoCommandHandler(
        IMapper mapper,
        IServicoEdicaoLivro servicoEdicaoLivro,
        IServicoListagemAutor servicoListagemAutores,
        IServicoListagemAssunto servicoListagemAssuntos,
        IServicoListagemLocalVenda servicoListagemLocaisVenda)
    {
        _servicoEdicaoLivro = servicoEdicaoLivro;
        _mapper = mapper;
        _servicoListagemAutores = servicoListagemAutores;
        _servicoListagemAssuntos = servicoListagemAssuntos;
        _servicoListagemLocaisVenda = servicoListagemLocaisVenda;
    }

    public async Task<Result<LivroEdicaoCommandResult>> Handle(LivroEdicaoCommand request, CancellationToken cancellationToken)
    {
        Result<LivroEdicaoCommandResult> result = new();

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

        var localisVenda = await _servicoListagemLocaisVenda.ListarAsync(new LocalVendaListaFiltro { Ids = request.LocaisVenda.Select(prop => prop.IdLocalVenda).ToList() });

        if (localisVenda is null || !localisVenda.Itens!.Any())
        {
            result.AddResultadoAcao(EResultadoAcaoServico.NaoEncontrado);
            result.AddNotification(nameof(Dominio.Entidades.LocalVenda), Mensagens.LocalVendaNaoEncontrado);

            return await Task.FromResult(result);
        }

        var livro = _mapper.Map<Dominio.Entidades.Livro>(request);
        livro.AlterarAutores(autores.Itens!.ToList());
        livro.AlterarAssuntos(assuntos.Itens!.ToList());

        var livrosLocaisVenda = new List<LivroLocalVenda>();
        request.LocaisVenda.ForEach(item =>
        {
            livrosLocaisVenda.Add(
                new LivroLocalVenda { 
                    LocalVendaId = item.IdLocalVenda, 
                    Valor = item.Valor 
                }
            );
        });

        livro.AlterarPrecoCatalogo(livrosLocaisVenda);

        var livroEditado = await _servicoEdicaoLivro.EditarAsync(livro, cancellationToken);

        result.AddResultadoAcao(_servicoEdicaoLivro.ResultadoAcao);
        result.AddNotifications(_servicoEdicaoLivro.Notifications);

        if (_servicoEdicaoLivro.ResultadoAcao != EResultadoAcaoServico.Suceso)
        {
            return await Task.FromResult(result);
        }

        result.Data = _mapper.Map<LivroEdicaoCommandResult>(livroEditado);

        return await Task.FromResult(result);
    }
}