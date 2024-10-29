using Livros.Aplicacao.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace Livros.Aplicacao.CasosUso.Autor.Editar;

public record AutorEdicaoCommand : IRequest<Result<AutorEdicaoCommandResult>>
{
    /// <summary>
    /// Identificador do autor
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }

    /// <summary>o
    /// Descrição do autor
    /// </summary>
    public string Nome { get; set; }
}