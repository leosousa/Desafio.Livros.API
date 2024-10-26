using Bogus;
using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;

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
}