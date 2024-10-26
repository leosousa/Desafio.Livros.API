using FluentValidation;
using Livros.Dominio.Contratos;
using OneOf;

namespace Livros.Dominio.Servicos.Assunto.Cadastrar;

public class ServicoCadastroAssunto : IServicoCadastroAssunto
{
    private readonly IValidator<Entidades.Assunto> _validator;
    private readonly IRepositorioAssunto _repositorio;

    public ServicoCadastroAssunto(
        IValidator<Entidades.Assunto> validator, 
        IRepositorioAssunto repositorio)
    {
        _validator = validator;
        _repositorio = repositorio;
    }

    public async Task<OneOf<Entidades.Assunto, AssuntoErro>> CadastrarAsync(Entidades.Assunto assunto, CancellationToken cancellationToken)
    {
        if (assunto is null)
        {
            return AssuntoErro.NaoInformado;
        }

        var validationResult = _validator.Validate(assunto);

        if (!validationResult.IsValid)
        {
            return AssuntoErro.Invalido;
        }

        var assuntoCadastrado = await _repositorio.CadastrarAsync(assunto);

        if (assuntoCadastrado is null)
        {
            return AssuntoErro.Erro;
        }

        return assuntoCadastrado;
    }
}