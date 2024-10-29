using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Cadastrar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroCadastroCommandMock : Faker<LivroCadastroCommand>
{
    private LivroCadastroCommandMock() : base("pt_BR")
    {
        RuleFor(livro => livro.Descricao, faker => faker.Lorem.Random.String(1, 20));
    }

    public static LivroCadastroCommand? GerarObjetoNulo() => null;

    public static LivroCadastroCommand GerarObjetoValido()
    {
        return new LivroCadastroCommandMock().Generate();
    }

    public static LivroCadastroCommand GerarObjetoInvalido()
    {
        var livroInvalido = GerarObjetoValido();

        livroInvalido.Descricao = new Faker().Lorem.Random.String(21, 100);

        return livroInvalido;
    }
}