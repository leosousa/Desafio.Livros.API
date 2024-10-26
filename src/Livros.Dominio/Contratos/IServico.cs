using Flunt.Notifications;

namespace Livros.Dominio.Contratos;

public interface IServico
{
    IReadOnlyCollection<Notification> Notifications { get; }
}