using Bogus;
using Livros.Dominio.Entidades;

namespace Livros.TesteUnitario.Mocks.Dominio.Entidades;

public class LivroMock : Faker<Livro>
{
    private LivroMock() : base("pt_BR")
    {
        RuleFor(autor => autor.Id, faker => faker.Random.Int(min: 1));

        RuleFor(autor => autor.Titulo, faker => faker.Lorem.Random.String(1, Livro.TITULO_MAXIMO_CARACTERES));

        RuleFor(autor => autor.Editora, faker => faker.Lorem.Random.String(1, Livro.EDITORA_MAXIMO_CARACTERES));

        RuleFor(autor => autor.Edicao, faker => faker.Lorem.Random.Int(min: 1, max: 100));

        RuleFor(autor => autor.AnoPublicacao, faker => faker.Lorem.Random.Int(min: 0, max: DateTime.Now.Year));

        RuleFor(autor => autor.Autores, AutorMock.GerarObjetoLista());

        RuleFor(autor => autor.Assuntos, AssuntoMock.GerarObjetoLista());
    }

    public static Livro? GerarObjetoNulo() => null;

    public static Livro GerarObjeto()
    {
        return new LivroMock()
            .CustomInstantiator(faker =>
                new Livro(
                    titulo: faker.Lorem.Random.String(1, Livro.TITULO_MAXIMO_CARACTERES),
                    editora: faker.Lorem.Random.String(1, Livro.EDITORA_MAXIMO_CARACTERES),
                    edicao: faker.Lorem.Random.Int(min: 1, max: 100),
                    anoPublicacao: faker.Lorem.Random.Int(min: 1, max: DateTime.Now.Year)
                )
            )
            .Generate();
    }

    public static Livro GerarObjetoValido()
    {
        return GerarObjeto();
    }

    public static List<Livro> GerarObjetoLista(int quantidade = 10)
    {
        var lista = new List<Livro>();

        for (var index = 0; index < quantidade; index++)
        {
            lista.Add(GerarObjeto());
        }

        return lista;
    }
}