using Livros.Aplicacao.DTOs;

namespace Livros.Aplicacao.CasosUso.Livro.Listar;

public class LivroListaPaginadaQueryResult : ListaPaginadaQueryResult
{
    public IEnumerable<LivroItemResult>? Itens { get; init; }
}

public class LivroItemResult
{
    /// <summary>
    /// Identificador do livro
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descrição do livro
    /// </summary>
    public string Descricao { get; set; }
}