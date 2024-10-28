using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.BuscarPorId;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Assunto;

public class AssuntoBuscaPorIdQueryResultMock : Faker<AssuntoBuscaPorIdQueryResult>
{
    private AssuntoBuscaPorIdQueryResultMock() : base("pt_BR")
    {
        RuleFor(entidade => entidade.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(entidade => entidade.Descricao, faker => faker.Lorem.Random.String(1, 255));
    }

    public static AssuntoBuscaPorIdQueryResult GerarObjeto()
    {
        return new AssuntoBuscaPorIdQueryResultMock().Generate();
    }
}