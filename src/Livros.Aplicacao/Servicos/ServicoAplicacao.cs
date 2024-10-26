using Flunt.Notifications;

namespace Livros.Aplicacao.Base;

public abstract class ServicoAplicacao : Notifiable<Notification>
{
    public void AddNotifications(FluentValidation.Results.ValidationResult validationResult)
    {
        validationResult.Errors.ForEach(erro =>
        {
            AddNotification(erro.PropertyName, erro.ErrorMessage);
        });
    }
}