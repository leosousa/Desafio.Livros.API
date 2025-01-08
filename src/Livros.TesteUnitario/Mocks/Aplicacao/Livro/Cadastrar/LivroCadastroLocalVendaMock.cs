using Bogus;
using Flunt.Notifications;
using Livros.Aplicacao.CasosUso.Livro.Cadastrar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro.Cadastrar;

public class LivroCadastroLocalVendaMock : Faker<LivroCadastroLocalVenda>
{
    private LivroCadastroLocalVendaMock() : base("pt_BR")
    {
        RuleFor(livroLocalVenda => livroLocalVenda.IdLocalVenda, faker => faker.Lorem.Random.Int(min: 1, max: 100));

        RuleFor(livroLocalVenda => livroLocalVenda.Valor, faker => faker.Finance.Amount(min: 1, max: 100));
    }

    public static LivroCadastroLocalVenda GerarObjeto()
    {
        return new LivroCadastroLocalVendaMock().Generate();
    }

    public static List<LivroCadastroLocalVenda> GerarObjetoLista()
    {
        return new List<LivroCadastroLocalVenda>
        {
            GerarObjeto(),
            GerarObjeto(),
            GerarObjeto(),
        };
    }
}