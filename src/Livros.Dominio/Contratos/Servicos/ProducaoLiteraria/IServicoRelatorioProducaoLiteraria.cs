namespace Livros.Dominio.Contratos.Servicos.ProducaoLiteraria;

public interface IServicoRelatorioProducaoLiteraria
{
    Task<ValueObjects.ProducaoLiteraria> ListarAsync();
}