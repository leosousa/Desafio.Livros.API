namespace Livros.Dominio.Contratos.Servicos.Autor;

public interface IServicoDelecaoAutor : IServico
{
    Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
}