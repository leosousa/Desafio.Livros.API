using Bogus;
using Livros.Dominio.Entidades;

namespace Livros.TesteUnitario.Mocks.Dominio.Entidades;

public class AutorMock : Faker<Autor>
{
    private AutorMock() : base("pt_BR")
    {
        RuleFor(autor => autor.Id, faker => faker.Random.Int(min: 1));

        RuleFor(autor => autor.Nome, faker => faker.Lorem.Random.String(1, Autor.AUTOR_NOME_MAXIMO_CARACTERES));
    }

    public static Autor? GerarObjetoNulo() => null;

    public static Autor GerarObjetoInvalido()
    {
        var assuntoInvalido = GerarObjetoValido();

        assuntoInvalido.AlterarNome(new Faker().Lorem.Random.String(21, 100));

        return assuntoInvalido;
    }

    public static Autor GerarObjetoValido()
    {
        return new AutorMock()
            .CustomInstantiator(faker => new Autor(faker.Random.String(1, Autor.AUTOR_NOME_MAXIMO_CARACTERES)))
            .Generate();
    }

    public static IEnumerable<Autor> GerarObjetoLista(int quantidade = 10)
    {
        var lista = new List<Autor>();

        for (var index = 0; index < quantidade; index++)
        {
            lista.Add(GerarObjetoValido());
        }

        return lista;
    }
}