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
    /// Título do livro
    /// </summary>
    public string Titulo { get; set; }

    /// <summary>
    /// Editora do livro
    /// </summary>
    public string Editora { get; set; }

    /// <summary>
    /// Edição do livro
    /// </summary>
    public int Edicao { get; set; }

    /// <summary>
    /// Ano de publicação do livro
    /// </summary>
    public int AnoPublicacao { get; set; }
}