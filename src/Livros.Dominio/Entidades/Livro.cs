namespace Livros.Dominio.Entidades;

/// <summary>
/// Armazena um livro
/// </summary>
public class Livro : Entidade
{
    protected Livro()
    {
        // Requerido pelo EntityFramework em relacionamentos
    }

    public Livro(string titulo,
        string editora,
        int edicao,
        int anoPublicacao,
        List<Autor> autores = null,
        List<Assunto> assuntos = null)
    {
        Titulo = titulo;
        Editora = editora;
        Edicao = edicao;
        AnoPublicacao = anoPublicacao;
        Autores = autores ?? new List<Autor>();
        Assuntos = assuntos ??new List<Assunto>();
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
    public List<Autor> Autores { get; set; }

    /// <summary>
    /// Assuntos em que o livro pode ser categorizado
    /// </summary>
    public List<Assunto> Assuntos { get; set; }

    /// <summary>
    /// Locais de venda em que o livro pode ser comprado
    /// </summary>
    public List<LivroLocalVenda> LocaisVenda { get; set; }

    public void AlterarTitulo(string titulo)
    {
        Titulo = titulo;
    }

    public void AlterarEditora(string editora)
    {
        Editora = editora;
    }

    public void AlterarEdicao(int edicao)
    {
        Edicao = edicao;
    }

    public void AlterarAnoPublicacao(int anoPublicacao)
    {
        AnoPublicacao = anoPublicacao;
    }

    public void AlterarAutores(List<Autor> autores)
    {
        Autores = autores;
    }

    public void AdicionarAutor(Autor autor)
    {
        Autores?.Add(autor);
    }

    public void AlterarAssuntos(List<Assunto> assuntos)
    {
        Assuntos = assuntos;
    }

    public void AdicionarAssunto(Assunto assunto)
    {
        Assuntos?.Add(assunto);
    }

    public void AlterarPrecoCatalogo(List<LivroLocalVenda> locaisVenda)
    {
        LocaisVenda = locaisVenda;
    }

    #region
    public const int TITULO_MAXIMO_CARACTERES = 40;
    public const int EDITORA_MAXIMO_CARACTERES = 40;
    #endregion
}