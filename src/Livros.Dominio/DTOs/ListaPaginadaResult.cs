using Livros.Dominio.Entidades;

namespace Livros.Dominio.DTOs;

public class ListaPaginadaResult<T> where T : Entidade
{
    public IEnumerable<T>? Itens { get; init; }

    public int NumeroPagina { get; init; }

    public int TamanhoPagina { get; init; }

    public int TotalRegistros { get; init; }

    public int TotalPaginas { get; init; }
}