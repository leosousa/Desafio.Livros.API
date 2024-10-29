using Bogus;
using Livros.Dominio.Entidades;
using Livros.Dominio.Servicos.Autor.Listar;

namespace Livros.TesteUnitario.Mocks.Dominio.DTOs;

public class AutorListaFiltroMock : Faker<AutorListaFiltro>
{
    private AutorListaFiltroMock() : base("pt_BR")
    {

        RuleFor(entidade => entidade.Nome, faker => faker.Lorem.Random.String(1, Autor.AUTOR_NOME_MAXIMO_CARACTERES));
    }

    public static AutorListaFiltro GerarObjeto()
    {
        return new AutorListaFiltroMock().Generate();
    }

    public static AutorListaFiltro? GerarObjetoNulo() => null;
}