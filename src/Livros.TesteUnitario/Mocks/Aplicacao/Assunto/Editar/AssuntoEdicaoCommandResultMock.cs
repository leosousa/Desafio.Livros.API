using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.Editar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Assunto;

public class AssuntoEdicaoCommandResultMock : Faker<AssuntoEdicaoCommandResult>
{
    private AssuntoEdicaoCommandResultMock() : base("pt_BR")
    {
        RuleFor(assunto => assunto.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(assunto => assunto.Descricao, faker => faker.Lorem.Random.String(1, 255));
    }

    public static AssuntoEdicaoCommandResult GerarObjeto()
    {
        return new AssuntoEdicaoCommandResultMock().Generate();
    }
}