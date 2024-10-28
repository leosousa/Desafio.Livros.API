namespace Livros.Dominio.Contratos.Servicos.Assunto;

public interface IServicoBuscaAssuntoPorId : IServico
{
    Task<Entidades.Assunto?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
}