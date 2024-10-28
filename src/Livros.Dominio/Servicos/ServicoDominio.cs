using Flunt.Notifications;
using Livros.Dominio.Enumeracoes;

namespace Livros.Dominio.Servicos;

public abstract class ServicoDominio : Notifiable<Notification>
{

    public EResultadoAcaoServico ResultadoAcao { get; private set; }

    public void AddResultadoAcao(EResultadoAcaoServico resultadoAcao)
    {
        ResultadoAcao = resultadoAcao;
    }

    public void AddNotifications(FluentValidation.Results.ValidationResult validationResult)
    {
        validationResult.Errors.ForEach(erro =>
        {
            AddNotification(erro.PropertyName, erro.ErrorMessage);
        });
    }
}