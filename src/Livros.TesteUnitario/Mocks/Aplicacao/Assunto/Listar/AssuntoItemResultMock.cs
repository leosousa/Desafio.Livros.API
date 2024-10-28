using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.Listar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Assunto;

public class AssuntoItemResultMock : Faker<AssuntoItemResult>
{
    private AssuntoItemResultMock() : base("pt_BR")
    {
        RuleFor(entidade => entidade.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(entidade => entidade.Descricao, faker => faker.Lorem.Random.String(1, 255));
    }

    public static AssuntoItemResult GerarObjeto()
    {
        return new AssuntoItemResultMock().Generate();
    }

    public static IEnumerable<AssuntoItemResult> GerarObjetoLista()
    {
        return new List<AssuntoItemResult>()
        {
            GerarObjeto(),
            GerarObjeto(),
            GerarObjeto()
        };
    }
}