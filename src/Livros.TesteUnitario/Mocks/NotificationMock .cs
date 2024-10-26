using Bogus;
using Flunt.Notifications;

namespace Livros.TesteUnitario.Mocks;
public class NotificationMock : Faker<Notification>
{
    private NotificationMock() : base("pt_BR")
    {
        RuleFor(notification => notification.Key, faker => faker.Lorem.Random.String(1, 255));

        RuleFor(notification => notification.Message, faker => faker.Lorem.Random.String(1, 255));
    }

    public static Notification GerarObjeto()
    {
        return new NotificationMock().Generate();
    }

    public static Notification GerarNotificacao(string propriedade, string mensagem)
    {
        return new Notification(propriedade, mensagem);
    }

    public static IReadOnlyCollection<Notification> GerarObjetoLista()
    {
        return new List<Notification>
        {
            GerarObjeto(),
            GerarObjeto(),
            GerarObjeto(),
        }.AsReadOnly();
    }

    public static IReadOnlyCollection<Notification> GerarListaVazia()
    {
        return new List<Notification>().AsReadOnly();
    }

    //public static IReadOnlyCollection<Notification> GerarObjetoLista()
    //{
    //    return new List<Notification>
    //    {
    //        GerarObjeto(),
    //        GerarObjeto(),
    //        GerarObjeto(),
    //    }.AsReadOnly();
    //}

    public static IReadOnlyList<Notification> GerarObjetoLista(string propriedade, string mensagem)
    {
        return new List<Notification>
        {
            GerarNotificacao(propriedade, mensagem)
        };
    }
}