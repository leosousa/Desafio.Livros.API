using Livros.Aplicacao.DTOs;

namespace Livros.Aplicacao.CasosUso.Assunto.Listar;

public class AssuntoListaPaginadaQueryResult : ListaPaginadaQueryResult
{
    public IEnumerable<AssuntoItemResult>? Itens { get; init; }
}

public class AssuntoItemResult
{
    /// <summary>
    /// Identificador do assunto
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descrição do assunto
    /// </summary>
    public string Descricao { get; set; }

    /// <summary>
    /// Quantidade de livros associados
    /// </summary>
    public bool PossuiLivrosAssociados { get; set; }
}