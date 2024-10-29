using Bogus;
using Livros.Dominio.DTOs;
using Livros.Dominio.Entidades;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;

namespace Livros.TesteUnitario.Mocks.Dominio.DTOs;

public class AutorListaPaginadaResultMock : Faker<ListaPaginadaResult<Autor>>
{
    private AutorListaPaginadaResultMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(assunto => assunto.TotalPaginas, faker => faker.Random.Int(min: 1));

        RuleFor(assunto => assunto.TotalRegistros, faker => faker.Random.Int(min: 1));

        RuleFor(assunto => assunto.Itens, AutorMock.GerarObjetoLista());
    }

    public static ListaPaginadaResult<Autor> GerarObjeto()
    {
        return new AutorListaPaginadaResultMock().Generate();
    }

    public static ListaPaginadaResult<Autor> GerarListaSemItens()
    {
        return new ListaPaginadaResult<Autor>
        {
            Itens = new List<Autor>(),
            NumeroPagina = 1,
            TamanhoPagina = 10,
            TotalPaginas = 1,
            TotalRegistros = 0
        };
    }
}