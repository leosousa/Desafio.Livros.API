using Livros.Dominio.Servicos.Assunto.Cadastrar;
using OneOf;

namespace Livros.Dominio.Contratos;

public interface IServicoCadastroAssunto
{
    Task<OneOf<Entidades.Assunto, AssuntoErro>> CadastrarAsync(Entidades.Assunto assunto, CancellationToken cancellationToken);
}