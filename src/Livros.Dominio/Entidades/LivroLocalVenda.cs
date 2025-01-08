namespace Livros.Dominio.Entidades;

public class LivroLocalVenda
{
    /// <summary>
    /// Identificador do livro
    /// </summary>
    public int LivroId { get; set; }

    /// <summary>
    /// Referência do livro
    /// </summary>
    public Livro Livro { get; set; }

    /// <summary>
    /// Identificado do local de vena
    /// </summary>
    public int LocalVendaId { get; set; }

    /// <summary>
    /// Referência do local de venda
    /// </summary>
    public LocalVenda LocalVenda { get; set; }

    /// <summary>
    /// Valor de venda do livro no local
    /// </summary>
    public decimal Valor { get; set; }
}
