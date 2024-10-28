using Bogus;
using Livros.Dominio.Entidades;

namespace Livros.TesteUnitario.Mocks.Dominio.Entidades;

public class AssuntoMock : Faker<Assunto>
{
    private AssuntoMock() : base("pt_BR")
    {
        RuleFor(assunto => assunto.Id, faker => faker.Random.Int(min: 1));

        RuleFor(assunto => assunto.Descricao, faker => faker.Lorem.Random.String(1, 20));
    }

    public static Assunto? GerarObjetoNulo() => null;

    public static Assunto GerarObjetoInvalido()
    {
        var assuntoInvalido = GerarObjetoValido();

        assuntoInvalido.AlterarDescricao(new Faker().Lorem.Random.String(21, 100));

        return assuntoInvalido;
    }

    public static Assunto GerarObjetoValido()
    {
        return new AssuntoMock()
            .CustomInstantiator(faker => new Assunto(faker.Random.String(1, 20)))
            .Generate();
    }

    public static IEnumerable<Assunto> GerarObjetoLista(int quantidade = 10)
    {
        var lista = new List<Assunto>();

        for (var index = 0; index < quantidade; index++)
        {
            lista.Add(GerarObjetoValido());
        }

        return lista;
    }
}