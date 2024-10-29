using Flunt.Notifications;
using Livros.Dominio.Enumeracoes;

namespace Livros.Dominio.Contratos;

public interface IServico
{
    EResultadoAcaoServico ResultadoAcao { get; }

    bool IsValid { get; }

    IReadOnlyCollection<Notification> Notifications { get; }
}