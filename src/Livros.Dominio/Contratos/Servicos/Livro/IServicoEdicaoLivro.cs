namespace Livros.Dominio.Contratos.Servicos.Livro;

public interface IServicoEdicaoLivro : IServico
{
    Task<Entidades.Livro?> EditarAsync(Entidades.Livro livro, CancellationToken cancellationToken);
}