using Bogus;
using Livros.Aplicacao.CasosUso.Livro.BuscarPorId;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroBuscaPorIdQueryResultMock : Faker<LivroBuscaPorIdQueryResult>
{
    private LivroBuscaPorIdQueryResultMock() : base("pt_BR")
    {
        RuleFor(entidade => entidade.Id, faker => faker.Lorem.Random.Int(min: 1));
    }

    public static LivroBuscaPorIdQueryResult GerarObjeto()
    {
        return new LivroBuscaPorIdQueryResultMock().Generate();
    }
}