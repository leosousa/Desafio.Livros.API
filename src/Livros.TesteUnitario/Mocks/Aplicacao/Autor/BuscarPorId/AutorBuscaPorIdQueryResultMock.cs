using Bogus;
using Livros.Aplicacao.CasosUso.Autor.BuscarPorId;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Autor;

public class AutorBuscaPorIdQueryResultMock : Faker<AutorBuscaPorIdQueryResult>
{
    private AutorBuscaPorIdQueryResultMock() : base("pt_BR")
    {
        RuleFor(entidade => entidade.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(entidade => entidade.Nome, faker => faker.Lorem.Random.String(1, 255));
    }

    public static AutorBuscaPorIdQueryResult GerarObjeto()
    {
        return new AutorBuscaPorIdQueryResultMock().Generate();
    }
}