using Bogus;
using Livros.Aplicacao.CasosUso.Autor.Listar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Autor;

public class AutorListaPaginadaQueryResultMock : Faker<AutorListaPaginadaQueryResult>
{
    private AutorListaPaginadaQueryResultMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(lista => lista.TotalPaginas, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.TotalRegistros, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.Itens, AutorItemResultMock.GerarObjetoLista());
    }

    public static AutorListaPaginadaQueryResult GerarObjeto()
    {
        return new AutorListaPaginadaQueryResultMock().Generate();
    }

    public static AutorListaPaginadaQueryResult GerarObjetoSemItens()
    {
        return new AutorListaPaginadaQueryResult
        {
            Itens = new List<AutorItemResult>(),
            NumeroPagina = 1,
            TamanhoPagina = 10,
            TotalPaginas = 1,
            TotalRegistros = 0
        };
    }
}