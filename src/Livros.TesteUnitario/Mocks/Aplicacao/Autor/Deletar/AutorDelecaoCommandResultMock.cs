using Bogus;
using Livros.Aplicacao.CasosUso.Autor.Deletar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Autor.Deletar;

public class AutorDelecaoCommandResultMock : Faker<AutorDelecaoCommandResult>
{
    private AutorDelecaoCommandResultMock() : base("pt_BR")
    {
    }

    public static AutorDelecaoCommandResult GerarObjeto()
    {
        return new AutorDelecaoCommandResultMock().Generate();
    }
}