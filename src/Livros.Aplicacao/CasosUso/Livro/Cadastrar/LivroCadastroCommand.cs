using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.Livro.Cadastrar;

public record LivroCadastroCommand : IRequest<Result<LivroCadastroCommandResult>>
{
    /// <summary>o
    /// Titulo do livro
    /// </summary>
    public string Titulo { get; set; }

    /// <summary>o
    /// Editora do livro
    /// </summary>
    public string Editora { get; set; }

    /// <summary>o
    /// Edição do livro
    /// </summary>
    public int Edicao { get; set; }

    /// <summary>o
    /// Ano de pubçica do livro
    /// </summary>
    public int AnoPublicacao { get; set; }

    /// <summary>
    /// Autores do livro
    /// </summary>
    public List<int> Autores { get; set; }

    /// <summary>
    /// Assuntos do livro
    /// </summary>
    public List<int> Assuntos { get; set; }
}