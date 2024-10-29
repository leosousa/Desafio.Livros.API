using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Listar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroListaPaginadaQueryResultMock : Faker<LivroListaPaginadaQueryResult>
{
    private LivroListaPaginadaQueryResultMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(lista => lista.TotalPaginas, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.TotalRegistros, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.Itens, LivroItemResultMock.GerarObjetoLista());
    }

    public static LivroListaPaginadaQueryResult GerarObjeto()
    {
        return new LivroListaPaginadaQueryResultMock().Generate();
    }

    public static LivroListaPaginadaQueryResult GerarObjetoSemItens()
    {
        return new LivroListaPaginadaQueryResult
        {
            Itens = new List<LivroItemResult>(),
            NumeroPagina = 1,
            TamanhoPagina = 10,
            TotalPaginas = 1,
            TotalRegistros = 0
        };
    }
}