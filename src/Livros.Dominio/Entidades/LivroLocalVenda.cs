namespace Livros.Dominio.Entidades;

public class LivroLocalVenda
{
    /// <summary>
    /// Identificador do livro
    /// </summary>
    public int IdLivro { get; set; }

    /// <summary>
    /// Referência do livro
    /// </summary>
    public virtual Livro Livro { get; set; }

    /// <summary>
    /// Identificado do local de vena
    /// </summary>
    public int IdLocalVenda { get; set; }

    /// <summary>
    /// Referência do local de venda
    /// </summary>
    public virtual LocalVenda LocalVenda { get; set; }

    /// <summary>
    /// Valor de venda do livro no local
    /// </summary>
    public decimal Valor { get; set; }
}
