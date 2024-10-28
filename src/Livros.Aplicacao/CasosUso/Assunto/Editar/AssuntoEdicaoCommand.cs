﻿using Livros.Aplicacao.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace Livros.Aplicacao.CasosUso.Assunto.Editar;

public record AssuntoEdicaoCommand : IRequest<Result<AssuntoEdicaoCommandResult>>
{
    /// <summary>
    /// Identificador do assunto
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }

    /// <summary>
    /// Descrição do produto
    /// </summary>
    public string Descricao { get; set; }
}