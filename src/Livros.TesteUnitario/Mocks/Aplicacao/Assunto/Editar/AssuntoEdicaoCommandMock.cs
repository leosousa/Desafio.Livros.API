using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.Editar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Assunto;

public class AssuntoEdicaoCommandMock : Faker<AssuntoEdicaoCommand>
{
    private AssuntoEdicaoCommandMock() : base("pt_BR")
    {
        RuleFor(produto => produto.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(produto => produto.Descricao, faker => faker.Lorem.Random.String(1, 255));
    }

    public static AssuntoEdicaoCommand GerarObjeto()
    {
        return new AssuntoEdicaoCommandMock().Generate();
    }

    public static AssuntoEdicaoCommand? GerarObjetoNulo() => null;
}