namespace Livros.Dominio.Contratos.Servicos.Livro;

public interface IServicoBuscaLivroPorId : IServico
{
    Task<Entidades.Livro?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
}