using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.Listar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Assunto;

public class AssuntoListaPaginadaQueryMock : Faker<AssuntoListaPaginadaQuery>
{
    private AssuntoListaPaginadaQueryMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(entidade => entidade.Descricao, faker => faker.Lorem.Random.String(1, 255));
    }

    public static AssuntoListaPaginadaQuery GerarObjeto()
    {
        return new AssuntoListaPaginadaQueryMock().Generate();
    }

    public static AssuntoListaPaginadaQuery? GerarObjetoNulo() => null;
}