namespace Livros.Dominio.Contratos;

public interface IServicoCadastroAssunto : IServico
{
    Task<Entidades.Assunto?> CadastrarAsync(Entidades.Assunto assunto, CancellationToken cancellationToken);
}