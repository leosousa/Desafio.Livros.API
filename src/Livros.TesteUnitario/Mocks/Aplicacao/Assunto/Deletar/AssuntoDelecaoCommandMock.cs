using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.Deletar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Assunto.Deletar;

public class AssuntoDelecaoCommandMock : Faker<AssuntoDelecaoCommand>
{
    private AssuntoDelecaoCommandMock() : base("pt_BR")
    {
        RuleFor(assunto => assunto.Id, faker => faker.Lorem.Random.Int(min: 1));
    }

    public static AssuntoDelecaoCommand GerarObjeto()
    {
        return new AssuntoDelecaoCommandMock().Generate();
    }

    public static AssuntoDelecaoCommand? GerarObjetoNulo() => null;
}