using Livros.Dominio.Servicos.Assunto.Cadastrar;
using OneOf;

namespace Livros.Dominio.Contratos;

public interface IServicoCadastroAssunto : IServico
{
    Task<OneOf<Entidades.Assunto, CadastroAssuntoRetorno>> CadastrarAsync(Entidades.Assunto assunto, CancellationToken cancellationToken);
}