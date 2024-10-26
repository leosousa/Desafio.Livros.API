using FluentValidation;
using Flunt.Notifications;
using Livros.Dominio.Contratos;
using Livros.Dominio.Recursos;
using OneOf;

namespace Livros.Dominio.Servicos.Assunto.Cadastrar;

public class ServicoCadastroAssunto : ServicoDominio, IServicoCadastroAssunto
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

    public async Task<OneOf<Entidades.Assunto, CadastroAssuntoRetorno>> CadastrarAsync(Entidades.Assunto assunto, CancellationToken cancellationToken)
    {
        if (assunto is null)
        {
            this.AddNotification(nameof(Entidades.Assunto), Mensagens.AssuntoNaoInformado);

            return CadastroAssuntoRetorno.NaoInformado;
        }

        var validationResult = _validator.Validate(assunto);

        if (!validationResult.IsValid)
        {
            AddNotifications(validationResult);

            return CadastroAssuntoRetorno.Invalido;
        }

        var assuntoCadastrado = await _repositorio.CadastrarAsync(assunto);

        if (assuntoCadastrado is null)
        {
            return CadastroAssuntoRetorno.Erro;
        }

        return assuntoCadastrado;
    }
}