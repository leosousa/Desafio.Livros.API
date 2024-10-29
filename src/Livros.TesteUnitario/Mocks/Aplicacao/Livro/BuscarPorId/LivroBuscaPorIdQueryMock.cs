using Bogus;
using Livros.Aplicacao.CasosUso.Livro.BuscarPorId;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroBuscaPorIdQueryMock : Faker<LivroBuscaPorIdQuery>
{
    private LivroBuscaPorIdQueryMock() : base("pt_BR")
    {
        RuleFor(livro => livro.Id, faker => faker.Lorem.Random.Int(min: 1));
    }

    public static LivroBuscaPorIdQuery? GerarObjetoNulo() => null;

    public static LivroBuscaPorIdQuery GerarObjeto()
    {
        return new LivroBuscaPorIdQueryMock().Generate();
    }
}