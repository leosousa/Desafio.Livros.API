using Livros.Dominio.DTOs;

namespace Livros.Dominio.Servicos.LocalVenda.Listar;

public class LocalVendaListaFiltro : FiltroPaginacao
{
    public List<int>? Ids { get; set; }
    public string? Descricao { get; set; }
}