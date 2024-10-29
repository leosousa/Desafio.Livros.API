using Bogus;
using Livros.Aplicacao.CasosUso.Autor.Editar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Autor;

public class AutorEdicaoCommandResultMock : Faker<AutorEdicaoCommandResult>
{
    private AutorEdicaoCommandResultMock() : base("pt_BR")
    {
        RuleFor(autor => autor.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(autor => autor.Nome, faker => faker.Lorem.Random.String(1, 255));
    }

    public static AutorEdicaoCommandResult GerarObjeto()
    {
        return new AutorEdicaoCommandResultMock().Generate();
    }
}