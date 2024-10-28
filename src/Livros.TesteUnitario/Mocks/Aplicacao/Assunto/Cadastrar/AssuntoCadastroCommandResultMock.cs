using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Assunto;

public class AssuntoCadastroCommandResultMock : Faker<AssuntoCadastroCommandResult>
{
    private AssuntoCadastroCommandResultMock() : base("pt_BR")
    {
        RuleFor(assunto => assunto.Id, faker => faker.Lorem.Random.Int(1, int.MaxValue));
    }

    public static AssuntoCadastroCommandResult? GerarObjetoNulo() => null;

    public static AssuntoCadastroCommandResult GerarObjeto()
    {
        return new AssuntoCadastroCommandResultMock().Generate();
    }
}