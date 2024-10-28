using Flunt.Notifications;

namespace Livros.Dominio.Contratos;

public interface IServico
{
    bool IsValid { get; }

    IReadOnlyCollection<Notification> Notifications { get; }
}