using Flunt.Notifications;

namespace Livros.Aplicacao.DTOs;

public class ListaPaginadaQueryResult : Notifiable<Notification>
{
    public int NumeroPagina { get; init; }

    public int TamanhoPagina { get; init; }

    public int TotalRegistros { get; init; }

    public int TotalPaginas { get; init; }
}