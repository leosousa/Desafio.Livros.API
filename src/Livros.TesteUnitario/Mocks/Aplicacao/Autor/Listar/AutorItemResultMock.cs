using Bogus;
using Livros.Aplicacao.CasosUso.Autor.Listar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Autor;

public class AutorItemResultMock : Faker<AutorItemResult>
{
    private AutorItemResultMock() : base("pt_BR")
    {
        RuleFor(entidade => entidade.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(entidade => entidade.Nome, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Autor.AUTOR_NOME_MAXIMO_CARACTERES));
    }

    public static AutorItemResult GerarObjeto()
    {
        return new AutorItemResultMock().Generate();
    }

    public static IEnumerable<AutorItemResult> GerarObjetoLista()
    {
        return new List<AutorItemResult>()
        {
            GerarObjeto(),
            GerarObjeto(),
            GerarObjeto()
        };
    }
}