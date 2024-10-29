
namespace Livros.Dominio.Entidades;

/// <summary>
/// Armazena um autor de livros
/// </summary>
public class Autor : Entidade
{
    protected Autor()
    {
        // Requerido pelo EntityFramework em relacionamentos
    }

    public Autor(string nome)
    {
        Nome = nome;
    }

    /// <summary>
    /// Nome do autor
    /// </summary>
    public string Nome { get; private set; }

    /// <summary>
    /// Livros relacionados ao autor
    /// </summary>
    public List<Livro> Livros { get; private set; }

    public void AlterarNome(string nome)
    {
        Nome = nome;
    }

    #region Constantes
    public const int AUTOR_NOME_MAXIMO_CARACTERES = 40;
    #endregion
}