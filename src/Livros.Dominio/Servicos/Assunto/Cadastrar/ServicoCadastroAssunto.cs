using Livros.Dominio.Contratos;
using OneOf;

namespace Livros.Dominio.Servicos.Assunto.Cadastrar;

public class ServicoCadastroAssunto : IServicoCadastroAssunto
{
    public OneOf<Entidades.Assunto, AssuntoErro> CadastrarAsync(Entidades.Assunto assunto, CancellationToken cancellationToken)
    {
        if (assunto is null)
        {
            return AssuntoErro.NaoInformado;
        }

        return AssuntoErro.Invalido;
    }
}