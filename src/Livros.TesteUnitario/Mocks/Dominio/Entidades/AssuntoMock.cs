using Bogus;
using Livros.Dominio.Entidades;

namespace Livros.TesteUnitario.Mocks.Dominio.Entidades;

public class AssuntoMock : Faker<Assunto>
{
    private AssuntoMock() : base("pt_BR")
    {
        RuleFor(task => task.Id, faker => faker.Random.Int(min: 1));

        RuleFor(task => task.Descricao, faker => faker.Lorem.Random.String(1, 20));
    }

    public static Assunto? GerarObjetoNulo() => null;
}