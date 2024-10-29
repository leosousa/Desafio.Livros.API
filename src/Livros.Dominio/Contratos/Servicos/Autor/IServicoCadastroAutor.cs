namespace Livros.Dominio.Contratos;

public interface IServicoCadastroAutor : IServico
{
    Task<Entidades.Assunto?> CadastrarAsync(Entidades.Assunto assunto, CancellationToken cancellationToken);
}