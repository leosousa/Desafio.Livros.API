using Bogus;
using Livros.Aplicacao.CasosUso.Autor.Editar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Autor;

public class AutorEdicaoCommandMock : Faker<AutorEdicaoCommand>
{
    private AutorEdicaoCommandMock() : base("pt_BR")
    {
        RuleFor(autor => autor.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(autor => autor.Nome, faker => faker.Lorem.Random.String(1, 255));
    }

    public static AutorEdicaoCommand GerarObjeto()
    {
        return new AutorEdicaoCommandMock().Generate();
    }

    public static AutorEdicaoCommand? GerarObjetoNulo() => null;
}