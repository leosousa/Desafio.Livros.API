using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.Deletar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Assunto.Deletar;

public class AssuntoDelecaoCommandResultMock : Faker<AssuntoDelecaoCommandResult>
{
    private AssuntoDelecaoCommandResultMock() : base("pt_BR")
    {
    }

    public static AssuntoDelecaoCommandResult GerarObjeto()
    {
        return new AssuntoDelecaoCommandResultMock().Generate();
    }
}