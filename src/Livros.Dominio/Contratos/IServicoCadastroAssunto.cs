using Livros.Dominio.Entidades;
using Livros.Dominio.Servicos.Assunto.Cadastrar;
using OneOf;

namespace Livros.Dominio.Contratos;

public interface IServicoCadastroAssunto
{
    OneOf<Assunto, AssuntoErro> CadastrarAsync(Entidades.Assunto assunto, CancellationToken cancellationToken);
}