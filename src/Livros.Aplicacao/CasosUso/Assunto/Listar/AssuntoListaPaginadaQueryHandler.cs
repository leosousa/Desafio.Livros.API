using AutoMapper;
using Flunt.Notifications;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.DTOs;
using Livros.Dominio.Servicos.Assunto.Listar;
using MediatR;
using AssuntoEntidade = Livros.Dominio.Entidades.Assunto;

namespace Livros.Aplicacao.CasosUso.Assunto.Listar;

public class AssuntoListaPaginadaQueryHandler : Notifiable<Notification>,
    IRequestHandler<AssuntoListaPaginadaQuery, AssuntoListaPaginadaQueryResult?>
{
    private readonly IServicoListagemAssunto _servicoListagemAssunto;
    private readonly IMapper _mapper;

    public AssuntoListaPaginadaQueryHandler(IServicoListagemAssunto servicoListagemAssunto, IMapper mapper)
    {
        _servicoListagemAssunto = servicoListagemAssunto;
        _mapper = mapper;
    }

    public async Task<AssuntoListaPaginadaQueryResult?> Handle(AssuntoListaPaginadaQuery request, CancellationToken cancellationToken)
    {
        var filtros = _mapper.Map<AssuntoListaFiltro>(request);

        ListaPaginadaResult<AssuntoEntidade> assuntos = null;

        if (filtros is not null)
        {
            assuntos = await _servicoListagemAssunto.ListarAsync(filtros, request.NumeroPagina, request.TamanhoPagina);
        }
        else
        {
            assuntos = await _servicoListagemAssunto.ListarAsync(filtros!);
        }


        var result = _mapper.Map<AssuntoListaPaginadaQueryResult>(assuntos);

        return await Task.FromResult(result);
    }
}