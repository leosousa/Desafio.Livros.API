namespace Livros.Dominio.Entidades;

/// <summary>
/// Armazena um autor de livros
/// </summary>
public class Autor : Entidade
{
    public Autor(string nome)
    {
        Nome = nome;
    }

    /// <summary>
    /// Nome do autor
    /// </summary>
    public string Nome { get; private set; }
}