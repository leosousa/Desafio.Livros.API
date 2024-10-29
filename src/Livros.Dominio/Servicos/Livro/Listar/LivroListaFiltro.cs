using Livros.Dominio.DTOs;

namespace Livros.Dominio.Servicos.Livro.Listar;

public class LivroListaFiltro : FiltroPaginacao
{
    public string? Titulo { get; set; }

    public string? Editora { get; set; }

    public int? Edicao { get; set; }

    public int? AnoPublicacao { get; set; }

    public string? Assunto { get; set; }

    public string? Autor { get; set; }
}