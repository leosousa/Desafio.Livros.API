using Livros.Aplicacao.DTOs;

namespace Livros.Aplicacao.CasosUso.Autor.Listar;

public class AutorListaPaginadaQueryResult : ListaPaginadaQueryResult
{
    public IEnumerable<AutorItemResult>? Itens { get; init; }
}

public class AutorItemResult
{
    /// <summary>
    /// Identificador do autor
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descrição do autor
    /// </summary>
    public string Nome { get; set; }
}