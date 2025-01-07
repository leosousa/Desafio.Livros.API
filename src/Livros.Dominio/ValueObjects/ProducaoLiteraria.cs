namespace Livros.Dominio.ValueObjects;

public record ProducaoLiteraria(List<ProducaoLiterariaItem> Itens);

public record ProducaoLiterariaItem(Autor Autor, Livro Livro, string[] Assuntos);

public record Autor(string Nome);

public record Livro(string Titulo, int AnoPublicacao);