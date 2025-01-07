namespace Livros.Dominio.ValueObjects;

public class ProducaoLiteraria()
{
    public List<ProducaoLiterariaItem> Itens { get; set; }
}

public class ProducaoLiterariaItem
{
    public ProducaoLiterariaItemAutor Autor { get; set; }

    public ProducaoLiterariaItemLivro Livro { get; set; }

    public string[] Assuntos { get; set; }
}

public class ProducaoLiterariaItemAutor
{
    public string Nome { get; set; }
}

public class ProducaoLiterariaItemLivro
{
    public string Titulo { get; set; }

    public int AnoPublicacao { get; set; }
}