using Bogus;
using Livros.Dominio.DTOs;
using Livros.Dominio.Entidades;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;

namespace Livros.TesteUnitario.Mocks.Dominio.DTOs;

public class LivroListaPaginadaResultMock : Faker<ListaPaginadaResult<Livro>>
{
    private LivroListaPaginadaResultMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(assunto => assunto.TotalPaginas, faker => faker.Random.Int(min: 1));

        RuleFor(assunto => assunto.TotalRegistros, faker => faker.Random.Int(min: 1));

        RuleFor(assunto => assunto.Itens, LivroMock.GerarObjetoLista());
    }

    public static ListaPaginadaResult<Livro> GerarObjeto()
    {
        return new LivroListaPaginadaResultMock().Generate();
    }

    public static ListaPaginadaResult<Livro> GerarListaSemItens()
    {
        return new ListaPaginadaResult<Livro>
        {
            Itens = new List<Livro>(),
            NumeroPagina = 1,
            TamanhoPagina = 10,
            TotalPaginas = 1,
            TotalRegistros = 0
        };
    }
}