using Livros.Aplicacao.DTOs;

namespace Livros.Aplicacao.CasosUso.Assunto.Listar;

public class AssuntoListaPaginadaQueryResult : ListaPaginadaQueryResult
{
    public IEnumerable<AssuntoItemResult>? Itens { get; init; }
}

public class AssuntoItemResult
{
    /// <summary>
    /// Identificador do produto
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descrição do produto
    /// </summary>
    public string Descricao { get; set; }
}