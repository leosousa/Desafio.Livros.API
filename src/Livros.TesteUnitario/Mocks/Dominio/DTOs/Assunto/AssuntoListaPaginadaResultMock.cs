using Bogus;
using Livros.Dominio.DTOs;
using Livros.Dominio.Entidades;

namespace Livros.TesteUnitario.Mocks.Dominio.DTOs;

public class AssuntoListaPaginadaResultMock : Faker<ListaPaginadaResult<Assunto>>
{
    private AssuntoListaPaginadaResultMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(assunto => assunto.TotalPaginas, faker => faker.Random.Int(min: 1));

        RuleFor(assunto => assunto.TotalRegistros, faker => faker.Random.Int(min: 1));
    }

    public static ListaPaginadaResult<Assunto> GerarObjeto()
    {
        return new AssuntoListaPaginadaResultMock().Generate();
    }

    public static ListaPaginadaResult<Assunto> GerarListaSemItens()
    {
        return new ListaPaginadaResult<Assunto>
        {
            Itens = new List<Assunto>(),
            NumeroPagina = 1,
            TamanhoPagina = 10,
            TotalPaginas = 1,
            TotalRegistros = 0
        };
    }
}