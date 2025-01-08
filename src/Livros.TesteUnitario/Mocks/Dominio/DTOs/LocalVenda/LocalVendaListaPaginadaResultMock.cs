using Bogus;
using Livros.Dominio.DTOs;
using Livros.Dominio.Entidades;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;

namespace Livros.TesteUnitario.Mocks.Dominio.DTOs;

public class LocalVendaListaPaginadaResultMock : Faker<ListaPaginadaResult<LocalVenda>>
{
    private LocalVendaListaPaginadaResultMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(assunto => assunto.TotalPaginas, faker => faker.Random.Int(min: 1));

        RuleFor(assunto => assunto.TotalRegistros, faker => faker.Random.Int(min: 1));

        RuleFor(assunto => assunto.Itens, LocalVendaMock.GerarObjetoLista());
    }

    public static ListaPaginadaResult<LocalVenda> GerarObjeto()
    {
        return new LocalVendaListaPaginadaResultMock().Generate();
    }

    public static ListaPaginadaResult<LocalVenda> GerarListaSemItens()
    {
        return new ListaPaginadaResult<LocalVenda>
        {
            Itens = new List<LocalVenda>(),
            NumeroPagina = 1,
            TamanhoPagina = 10,
            TotalPaginas = 1,
            TotalRegistros = 0
        };
    }
}