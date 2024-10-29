using Bogus;
using Livros.Aplicacao.CasosUso.Autor.BuscarPorId;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Autor;

public class AutorBuscaPorIdQueryMock : Faker<AutorBuscaPorIdQuery>
{
    private AutorBuscaPorIdQueryMock() : base("pt_BR")
    {
        RuleFor(autor => autor.Id, faker => faker.Lorem.Random.Int(min: 1));
    }

    public static AutorBuscaPorIdQuery? GerarObjetoNulo() => null;

    public static AutorBuscaPorIdQuery GerarObjeto()
    {
        return new AutorBuscaPorIdQueryMock().Generate();
    }
}