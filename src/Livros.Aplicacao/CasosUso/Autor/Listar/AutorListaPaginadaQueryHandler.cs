using AutoMapper;
using Flunt.Notifications;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.DTOs;
using Livros.Dominio.Servicos.Autor.Listar;
using MediatR;
using AutorEntidade = Livros.Dominio.Entidades.Autor;

namespace Livros.Aplicacao.CasosUso.Autor.Listar;

public class AutorListaPaginadaQueryHandler : Notifiable<Notification>,
    IRequestHandler<AutorListaPaginadaQuery, AutorListaPaginadaQueryResult?>
{
    private readonly IServicoListagemAutor _servicoListagemAutor;
    private readonly IMapper _mapper;

    public AutorListaPaginadaQueryHandler(IServicoListagemAutor servicoListagemAutor, IMapper mapper)
    {
        _servicoListagemAutor = servicoListagemAutor;
        _mapper = mapper;
    }

    public async Task<AutorListaPaginadaQueryResult?> Handle(AutorListaPaginadaQuery request, CancellationToken cancellationToken)
    {
        var filtros = _mapper.Map<AutorListaFiltro>(request);

        ListaPaginadaResult<AutorEntidade> autors = null;

        if (filtros is not null)
        {
            autors = await _servicoListagemAutor.ListarAsync(filtros, request.NumeroPagina, request.TamanhoPagina);
        }
        else
        {
            autors = await _servicoListagemAutor.ListarAsync(filtros!);
        }


        var result = _mapper.Map<AutorListaPaginadaQueryResult>(autors);

        return await Task.FromResult(result);
    }
}