using Bogus;
using Livros.Aplicacao.CasosUso.Autor.Deletar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Autor.Deletar;

public class AutorDelecaoCommandMock : Faker<AutorDelecaoCommand>
{
    private AutorDelecaoCommandMock() : base("pt_BR")
    {
        RuleFor(autor => autor.Id, faker => faker.Lorem.Random.Int(min: 1));
    }

    public static AutorDelecaoCommand GerarObjeto()
    {
        return new AutorDelecaoCommandMock().Generate();
    }

    public static AutorDelecaoCommand? GerarObjetoNulo() => null;
}