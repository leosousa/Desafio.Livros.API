using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Deletar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro.Deletar;

public class LivroDelecaoCommandMock : Faker<LivroDelecaoCommand>
{
    private LivroDelecaoCommandMock() : base("pt_BR")
    {
        RuleFor(livro => livro.Id, faker => faker.Lorem.Random.Int(min: 1));
    }

    public static LivroDelecaoCommand GerarObjeto()
    {
        return new LivroDelecaoCommandMock().Generate();
    }

    public static LivroDelecaoCommand? GerarObjetoNulo() => null;
}