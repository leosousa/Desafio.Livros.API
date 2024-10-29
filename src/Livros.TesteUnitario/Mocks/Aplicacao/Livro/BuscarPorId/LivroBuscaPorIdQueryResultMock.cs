using Bogus;
using Livros.Aplicacao.CasosUso.Livro.BuscarPorId;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroBuscaPorIdQueryResultMock : Faker<LivroBuscaPorIdQueryResult>
{
    private LivroBuscaPorIdQueryResultMock() : base("pt_BR")
    {
        RuleFor(entidade => entidade.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(entidade => entidade.Descricao, faker => faker.Lorem.Random.String(1, 255));
    }

    public static LivroBuscaPorIdQueryResult GerarObjeto()
    {
        return new LivroBuscaPorIdQueryResultMock().Generate();
    }
}