using Bogus;
using Livros.Dominio.Entidades;

namespace Livros.TesteUnitario.Mocks.Dominio.Entidades;

public class LocalVendaMock : Faker<LocalVenda>
{
    private LocalVendaMock() : base("pt_BR")
    {
        RuleFor(autor => autor.Id, faker => faker.Random.Int(min: 1));

        RuleFor(autor => autor.Descricao, faker => faker.Lorem.Random.String(1, LocalVenda.DESCRICAO_MAXIMO_CARACTERES));
    }

    public static LocalVenda? GerarObjetoNulo() => null;

    public static LocalVenda GerarObjeto()
    {
        return new LocalVendaMock()
            .CustomInstantiator(faker =>
                new LocalVenda(
                    descricao: faker.Lorem.Random.String(1, LocalVenda.DESCRICAO_MAXIMO_CARACTERES)
                )
            )
            .Generate();
    }

    public static LocalVenda GerarObjetoValido()
    {
        return GerarObjeto();
    }

    public static List<LocalVenda> GerarObjetoLista(int quantidade = 10)
    {
        var lista = new List<LocalVenda>();

        for (var index = 0; index < quantidade; index++)
        {
            lista.Add(GerarObjeto());
        }

        return lista;
    }
}