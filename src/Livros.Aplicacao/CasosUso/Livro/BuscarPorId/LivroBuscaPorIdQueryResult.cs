namespace Livros.Aplicacao.CasosUso.Livro.BuscarPorId;

public class LivroBuscaPorIdQueryResult
{
    public int Id { get; set; }
    public string Titulo { get; set; }

    public string Editora { get; set; }

    public int Edicao { get; set; }

    public int AnoPublicacao { get; set; }

    public List<AutorResult> Autores { get; set; }

    public List<AssuntoResult> Assuntos { get; set; }
}

public class AutorResult
{
    public int Id { get; set; }
    public string Nome { get; set; }
}

public class AssuntoResult
{
    public int Id { get; set; }
    public string Descricao { get; set; }
}