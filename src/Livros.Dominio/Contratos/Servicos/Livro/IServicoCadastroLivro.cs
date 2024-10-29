namespace Livros.Dominio.Contratos;

public interface IServicoCadastroLivro : IServico
{
    Task<Entidades.Livro?> CadastrarAsync(Entidades.Livro livro, CancellationToken cancellationToken);
}