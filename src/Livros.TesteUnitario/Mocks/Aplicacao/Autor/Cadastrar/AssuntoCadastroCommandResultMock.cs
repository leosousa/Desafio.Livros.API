using Bogus;
using Livros.Aplicacao.CasosUso.Autor.Cadastrar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Autor;

public class AutorCadastroCommandResultMock : Faker<AutorCadastroCommandResult>
{
    private AutorCadastroCommandResultMock() : base("pt_BR")
    {
        RuleFor(autor => autor.Id, faker => faker.Lorem.Random.Int(1, int.MaxValue));
    }

    public static AutorCadastroCommandResult? GerarObjetoNulo() => null;

    public static AutorCadastroCommandResult GerarObjeto()
    {
        return new AutorCadastroCommandResultMock().Generate();
    }
}