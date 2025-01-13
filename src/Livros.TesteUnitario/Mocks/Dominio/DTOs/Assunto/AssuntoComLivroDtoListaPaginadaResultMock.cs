using Bogus;
using Livros.Dominio.DTOs;
using Livros.Dominio.DTOs.Assunto;

namespace Livros.TesteUnitario.Mocks.Dominio.DTOs;

public class AssuntoComLivroDtoListaPaginadaResultMock : Faker<ListaPaginadaResult<AssuntoComLivroDto>>
{
    private AssuntoComLivroDtoListaPaginadaResultMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(assunto => assunto.TotalPaginas, faker => faker.Random.Int(min: 1));

        RuleFor(assunto => assunto.TotalRegistros, faker => faker.Random.Int(min: 1));

        RuleFor(assunto => assunto.Itens, AssuntoComLivroDtoMock.GerarObjetoLista());
    }

    public static ListaPaginadaResult<AssuntoComLivroDto> GerarObjeto()
    {
        return new AssuntoComLivroDtoListaPaginadaResultMock().Generate();
    }

    public static ListaPaginadaResult<AssuntoComLivroDtoMock> GerarListaSemItens()
    {
        return new ListaPaginadaResult<AssuntoComLivroDtoMock>
        {
            Itens = new List<AssuntoComLivroDtoMock>(),
            NumeroPagina = 1,
            TamanhoPagina = 10,
            TotalPaginas = 1,
            TotalRegistros = 0
        };
    }
}