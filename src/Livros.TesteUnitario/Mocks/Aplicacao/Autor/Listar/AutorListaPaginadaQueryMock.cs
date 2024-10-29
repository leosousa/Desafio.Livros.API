using Bogus;
using Livros.Aplicacao.CasosUso.Autor.Listar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Autor;

public class AutorListaPaginadaQueryMock : Faker<AutorListaPaginadaQuery>
{
    private AutorListaPaginadaQueryMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(entidade => entidade.Nome, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Autor.AUTOR_NOME_MAXIMO_CARACTERES));
    }

    public static AutorListaPaginadaQuery GerarObjeto()
    {
        return new AutorListaPaginadaQueryMock().Generate();
    }

    public static AutorListaPaginadaQuery? GerarObjetoNulo() => null;
}