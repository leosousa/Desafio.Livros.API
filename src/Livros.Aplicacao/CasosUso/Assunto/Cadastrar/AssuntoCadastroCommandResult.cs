using Flunt.Notifications;

namespace Livros.Aplicacao.CasosUso.Assunto.Cadastrar;

public class AssuntoCadastroCommandResult : Notifiable<Notification>
{
    public int Id { get; set; }
}