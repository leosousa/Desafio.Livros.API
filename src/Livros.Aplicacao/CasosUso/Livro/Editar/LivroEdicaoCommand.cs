using Livros.Aplicacao.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace Livros.Aplicacao.CasosUso.Livro.Editar;

public record LivroEdicaoCommand : IRequest<Result<LivroEdicaoCommandResult>>
{
    /// <summary>
    /// Identificador do livro
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }

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

    /// <summary>
    /// Locais de venda do livro com preços do catálogo
    /// </summary>
    public List<LivroEdicaoLocalVenda> LocaisVenda { get; set; }
}

public record LivroEdicaoLocalVenda
{
    public int IdLocalVenda { get; set; }

    public decimal Valor { get; set; }
}