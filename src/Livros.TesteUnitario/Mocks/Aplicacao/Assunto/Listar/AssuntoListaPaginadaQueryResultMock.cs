using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.Listar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Assunto;

public class AssuntoListaPaginadaQueryResultMock : Faker<AssuntoListaPaginadaQueryResult>
{
    private AssuntoListaPaginadaQueryResultMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(lista => lista.TotalPaginas, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.TotalRegistros, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.Itens, AssuntoItemResultMock.GerarObjetoLista());
    }

    public static AssuntoListaPaginadaQueryResult GerarObjeto()
    {
        return new AssuntoListaPaginadaQueryResultMock().Generate();
    }

    public static AssuntoListaPaginadaQueryResult GerarObjetoSemItens()
    {
        return new AssuntoListaPaginadaQueryResult
        {
            Itens = new List<AssuntoItemResult>(),
            NumeroPagina = 1,
            TamanhoPagina = 10,
            TotalPaginas = 1,
            TotalRegistros = 0
        };
    }
}