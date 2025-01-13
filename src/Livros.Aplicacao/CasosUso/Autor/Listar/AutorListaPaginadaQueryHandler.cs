using AutoMapper;
using Flunt.Notifications;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.DTOs;
using Livros.Dominio.DTOs.Autor;
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

        ListaPaginadaResult<AutorComLivroDto> autores = null;

        if (filtros is not null)
        {
            autores = await _servicoListagemAutor.ListarComLivrosAsync(filtros, request.NumeroPagina, request.TamanhoPagina);
        }
        else
        {
            autores = await _servicoListagemAutor.ListarComLivrosAsync(filtros!);
        }


        var result = _mapper.Map<AutorListaPaginadaQueryResult>(autores);

        return await Task.FromResult(result);
    }
}