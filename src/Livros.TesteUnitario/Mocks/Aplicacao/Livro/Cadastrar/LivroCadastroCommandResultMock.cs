using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Cadastrar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroCadastroCommandResultMock : Faker<LivroCadastroCommandResult>
{
    private LivroCadastroCommandResultMock() : base("pt_BR")
    {
        RuleFor(livro => livro.Id, faker => faker.Lorem.Random.Int(1, int.MaxValue));
    }

    public static LivroCadastroCommandResult? GerarObjetoNulo() => null;

    public static LivroCadastroCommandResult GerarObjeto()
    {
        return new LivroCadastroCommandResultMock().Generate();
    }
}