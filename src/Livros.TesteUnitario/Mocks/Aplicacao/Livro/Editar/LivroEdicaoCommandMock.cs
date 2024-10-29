using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Editar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroEdicaoCommandMock : Faker<LivroEdicaoCommand>
{
    private LivroEdicaoCommandMock() : base("pt_BR")
    {
        RuleFor(livro => livro.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(livro => livro.Descricao, faker => faker.Lorem.Random.String(1, 255));
    }

    public static LivroEdicaoCommand GerarObjeto()
    {
        return new LivroEdicaoCommandMock().Generate();
    }

    public static LivroEdicaoCommand? GerarObjetoNulo() => null;
}