using AutoMapper;
using Flunt.Notifications;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.DTOs;
using Livros.Dominio.Servicos.Livro.Listar;
using MediatR;
using LivroEntidade = Livros.Dominio.Entidades.Livro;

namespace Livros.Aplicacao.CasosUso.Livro.Listar;

public class LivroListaPaginadaQueryHandler : Notifiable<Notification>,
    IRequestHandler<LivroListaPaginadaQuery, LivroListaPaginadaQueryResult?>
{
    private readonly IServicoListagemLivro _servicoListagemLivro;
    private readonly IMapper _mapper;

    public LivroListaPaginadaQueryHandler(IServicoListagemLivro servicoListagemLivro, IMapper mapper)
    {
        _servicoListagemLivro = servicoListagemLivro;
        _mapper = mapper;
    }

    public async Task<LivroListaPaginadaQueryResult?> Handle(LivroListaPaginadaQuery request, CancellationToken cancellationToken)
    {
        var filtros = _mapper.Map<LivroListaFiltro>(request);

        ListaPaginadaResult<LivroEntidade> livros = null;

        if (filtros is not null)
        {
            livros = await _servicoListagemLivro.ListarAsync(filtros, request.NumeroPagina, request.TamanhoPagina);
        }
        else
        {
            livros = await _servicoListagemLivro.ListarAsync(filtros!);
        }


        var result = _mapper.Map<LivroListaPaginadaQueryResult>(livros);

        return await Task.FromResult(result);
    }
}