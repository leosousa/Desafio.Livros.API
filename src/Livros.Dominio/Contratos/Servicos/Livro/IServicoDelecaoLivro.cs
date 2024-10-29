namespace Livros.Dominio.Contratos.Servicos.Livro;

public interface IServicoDelecaoLivro : IServico
{
    Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
}