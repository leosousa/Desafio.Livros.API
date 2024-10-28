using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.BuscarPorId;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Assunto;

public class AssuntoBuscaPorIdQueryMock : Faker<AssuntoBuscaPorIdQuery>
{
    private AssuntoBuscaPorIdQueryMock() : base("pt_BR")
    {
        RuleFor(assunto => assunto.Id, faker => faker.Lorem.Random.Int(min: 1));
    }

    public static AssuntoBuscaPorIdQuery? GerarObjetoNulo() => null;

    public static AssuntoBuscaPorIdQuery GerarObjeto()
    {
        return new AssuntoBuscaPorIdQueryMock().Generate();
    }
}