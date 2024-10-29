using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Editar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroEdicaoCommandResultMock : Faker<LivroEdicaoCommandResult>
{
    private LivroEdicaoCommandResultMock() : base("pt_BR")
    {
        RuleFor(livro => livro.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(livro => livro.Descricao, faker => faker.Lorem.Random.String(1, 255));
    }

    public static LivroEdicaoCommandResult GerarObjeto()
    {
        return new LivroEdicaoCommandResultMock().Generate();
    }
}