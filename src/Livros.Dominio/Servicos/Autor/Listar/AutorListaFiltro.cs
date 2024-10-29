using Livros.Dominio.DTOs;

namespace Livros.Dominio.Servicos.Autor.Listar;

public class AutorListaFiltro : FiltroPaginacao
{
    public string? Nome { get; set; }
}