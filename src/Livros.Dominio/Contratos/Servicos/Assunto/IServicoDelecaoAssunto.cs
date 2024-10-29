namespace Livros.Dominio.Contratos.Servicos.Assunto;

public interface IServicoDelecaoAssunto : IServico
{
    Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
}