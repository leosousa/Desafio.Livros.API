using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.Editar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Assunto;

public class AssuntoEdicaoCommandResultMock : Faker<AssuntoEdicaoCommandResult>
{
    private AssuntoEdicaoCommandResultMock() : base("pt_BR")
    {
        RuleFor(produto => produto.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(produto => produto.Descricao, faker => faker.Lorem.Random.String(1, 255));
    }

    public static AssuntoEdicaoCommandResult GerarObjeto()
    {
        return new AssuntoEdicaoCommandResultMock().Generate();
    }
}