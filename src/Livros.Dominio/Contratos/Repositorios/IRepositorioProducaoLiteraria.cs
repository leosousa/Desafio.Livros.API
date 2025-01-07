using Livros.Dominio.DTOs;
namespace Livros.Dominio.Contratos.Repositorios;

public interface IRepositorioProducaoLiteraria
{
    Task<IEnumerable<RelatorioProducaoLiterariaItem>> ListarProducaoLiteraria();
}