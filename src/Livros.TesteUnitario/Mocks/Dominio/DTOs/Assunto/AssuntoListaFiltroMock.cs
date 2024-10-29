using Bogus;
using Livros.Dominio.Servicos.Assunto.Listar;

namespace Livros.TesteUnitario.Mocks.Dominio.DTOs;

public class AssuntoListaFiltroMock : Faker<AssuntoListaFiltro>
{
    private AssuntoListaFiltroMock() : base("pt_BR")
    {

        RuleFor(entidade => entidade.Descricao, faker => faker.Lorem.Random.String(1, 255));
    }

    public static AssuntoListaFiltro GerarObjeto()
    {
        return new AssuntoListaFiltroMock().Generate();
    }

    public static AssuntoListaFiltro? GerarObjetoNulo() => null;
}