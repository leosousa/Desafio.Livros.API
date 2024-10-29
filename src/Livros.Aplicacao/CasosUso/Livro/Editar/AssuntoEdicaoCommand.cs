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
    /// Descrição do livro
    /// </summary>
    public string Descricao { get; set; }
}