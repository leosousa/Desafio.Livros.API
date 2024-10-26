using Flunt.Notifications;

namespace Livros.Dominio.Servicos;

public abstract class ServicoDominio : Notifiable<Notification>
{
    public void AddNotifications(FluentValidation.Results.ValidationResult validationResult)
    {
        validationResult.Errors.ForEach(erro =>
        {
            AddNotification(erro.PropertyName, erro.ErrorMessage);
        });
    }
}