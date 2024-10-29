using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Deletar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro.Deletar;

public class LivroDelecaoCommandResultMock : Faker<LivroDelecaoCommandResult>
{
    private LivroDelecaoCommandResultMock() : base("pt_BR")
    {
    }

    public static LivroDelecaoCommandResult GerarObjeto()
    {
        return new LivroDelecaoCommandResultMock().Generate();
    }
}