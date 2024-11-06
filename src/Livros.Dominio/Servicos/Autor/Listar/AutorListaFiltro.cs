using Livros.Dominio.DTOs;

namespace Livros.Dominio.Servicos.Autor.Listar;

public class AutorListaFiltro : FiltroPaginacao
{
    public List<int>? Ids { get; set; }
    public string? Nome { get; set; }
}