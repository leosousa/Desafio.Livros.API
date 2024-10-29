using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Autor.Listar;

public class AutorListaPaginadaQuery : FiltroListaPaginadaQuery, IRequest<AutorListaPaginadaQueryResult>
{
    public string? Nome { get; init; }
}