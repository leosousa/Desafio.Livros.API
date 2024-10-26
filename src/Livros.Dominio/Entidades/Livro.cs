namespace Livros.Dominio.Entidades;

/// <summary>
/// Armazena um livro
/// </summary>
public class Livro : Entidade
{
    public Livro(string titulo, 
        string editora, 
        int edicao, 
        int anoPublicacao, 
        ICollection<Autor> autores, 
        ICollection<Assunto> assuntos)
    {
        Titulo = titulo;
        Editora = editora;
        Edicao = edicao;
        AnoPublicacao = anoPublicacao;
        Autores = autores;
        Assuntos = assuntos;
    }

    /// <summary>
    /// Título do livro
    /// </summary>
    public string Titulo { get; private set; }

    /// <summary>
    /// Editora que publicou o livro
    /// </summary>
    public string Editora { get; private set; }

    /// <summary>
    /// Número da edição do livro
    /// </summary>
    public int Edicao { get; private set; }

    /// <summary>
    /// Ano de publicação do livro
    /// </summary>
    public int AnoPublicacao { get; private set; }

    /// <summary>
    /// Autores do livro
    /// </summary>
    public ICollection<Autor> Autores { get; private set; }

    /// <summary>
    /// Assuntos em que o livro pode ser categorizado
    /// </summary>
    public ICollection<Assunto> Assuntos { get; private set; }

}