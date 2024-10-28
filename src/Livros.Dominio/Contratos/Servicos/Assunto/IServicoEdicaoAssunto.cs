namespace Livros.Dominio.Contratos.Servicos.Assunto;

public interface IServicoEdicaoAssunto : IServico
{
    Task<Entidades.Assunto?> EditarAsync(Entidades.Assunto assunto, CancellationToken cancellationToken);
}