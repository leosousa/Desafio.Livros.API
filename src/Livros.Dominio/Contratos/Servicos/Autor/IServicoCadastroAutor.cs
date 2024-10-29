namespace Livros.Dominio.Contratos;

public interface IServicoCadastroAutor : IServico
{
    Task<Entidades.Autor?> CadastrarAsync(Entidades.Autor autor, CancellationToken cancellationToken);
}