using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Listar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroItemResultMock : Faker<LivroItemResult>
{
    private LivroItemResultMock() : base("pt_BR")
    {
        RuleFor(entidade => entidade.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(entidade => entidade.Descricao, faker => faker.Lorem.Random.String(1, 255));
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