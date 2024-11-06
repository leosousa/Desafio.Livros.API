using Flunt.Notifications;

namespace Livros.Aplicacao.CasosUso.Livro.Editar;

public class LivroEdicaoCommandResult
{
    /// <summary>
    /// Identificador do livro
    /// </summary>
    public int Id { get; set; }

    /// <summary>o
    /// Titulo do livro
    /// </summary>
    public string Titulo { get; set; }

    /// <summary>o
    /// Editora do livro
    /// </summary>
    public string Editora { get; set; }

    /// <summary>o
    /// Edição do livro
    /// </summary>
    public int Edicao { get; set; }

    /// <summary>o
    /// Ano de publicação do livro
    /// </summary>
    public int AnoPublicacao { get; set; }
}