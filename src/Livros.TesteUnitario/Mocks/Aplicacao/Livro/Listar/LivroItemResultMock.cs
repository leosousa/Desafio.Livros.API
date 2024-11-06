using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Listar;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroItemResultMock : Faker<LivroItemResult>
{
    private LivroItemResultMock() : base("pt_BR")
    {
        RuleFor(entidade => entidade.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(autor => autor.Titulo, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Livro.TITULO_MAXIMO_CARACTERES));

        RuleFor(autor => autor.Editora, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Livro.EDITORA_MAXIMO_CARACTERES));

        RuleFor(autor => autor.Edicao, faker => faker.Lorem.Random.Int(min: 1, max: 100));

        RuleFor(autor => autor.AnoPublicacao, faker => faker.Lorem.Random.Int(min: 0, max: DateTime.Now.Year));
    }

    public static LivroItemResult GerarObjeto()
    {
        return new LivroItemResultMock().Generate();
    }

    public static IEnumerable<LivroItemResult> GerarObjetoLista()
    {
        return new List<LivroItemResult>()
        {
            GerarObjeto(),
            GerarObjeto(),
            GerarObjeto()
        };
    }
}