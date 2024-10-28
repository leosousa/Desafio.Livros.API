using Livros.Dominio.DTOs;

namespace Livros.Dominio.Servicos.Assunto.Listar;

public class AssuntoListaFiltro : FiltroPaginacao
{
    public string? Descricao { get; set; }
}