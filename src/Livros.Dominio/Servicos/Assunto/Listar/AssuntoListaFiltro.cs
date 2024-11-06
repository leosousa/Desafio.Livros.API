using Livros.Dominio.DTOs;

namespace Livros.Dominio.Servicos.Assunto.Listar;

public class AssuntoListaFiltro : FiltroPaginacao
{
    public List<int>? Ids { get; set; }
    public string? Descricao { get; set; }
}