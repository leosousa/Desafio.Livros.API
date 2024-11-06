using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Livro.Listar;

public class LivroListaPaginadaQuery : FiltroListaPaginadaQuery, IRequest<LivroListaPaginadaQueryResult>
{
    public string? Titulo { get; init; }

    public string? Editora { get; init; }

    public int? Edicao { get; init; }

    public int? AnoPublicacao { get; init; }

    public string? Assunto { get; init; }

    public string? Autor { get; init; }
}