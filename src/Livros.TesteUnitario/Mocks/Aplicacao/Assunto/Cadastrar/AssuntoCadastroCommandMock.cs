using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Assunto;

public class AssuntoCadastroCommandMock : Faker<AssuntoCadastroCommand>
{
    private AssuntoCadastroCommandMock() : base("pt_BR")
    {
        RuleFor(assunto => assunto.Descricao, faker => faker.Lorem.Random.String(1, 20));
    }

    public static AssuntoCadastroCommand? GerarObjetoNulo() => null;

    public static AssuntoCadastroCommand GerarObjetoValido()
    {
        return new AssuntoCadastroCommandMock().Generate();
    }

    public static AssuntoCadastroCommand GerarObjetoInvalido()
    {
        var assuntoInvalido = GerarObjetoValido();

        assuntoInvalido.Descricao = new Faker().Lorem.Random.String(21, 100);

        return assuntoInvalido;
    }
}