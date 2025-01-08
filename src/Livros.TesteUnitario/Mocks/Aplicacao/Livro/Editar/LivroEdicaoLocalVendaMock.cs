using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Editar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro.Editar;

public class LivroEdicaoLocalVendaMock : Faker<LivroEdicaoLocalVenda>
{
    private LivroEdicaoLocalVendaMock() : base("pt_BR")
{
    RuleFor(livroLocalVenda => livroLocalVenda.IdLocalVenda, faker => faker.Lorem.Random.Int(min: 1, max: 100));

    RuleFor(livroLocalVenda => livroLocalVenda.Valor, faker => faker.Finance.Amount(min: 1, max: 100));
}

public static LivroEdicaoLocalVenda GerarObjeto()
{
    return new LivroEdicaoLocalVendaMock().Generate();
}

public static List<LivroEdicaoLocalVenda> GerarObjetoLista()
{
    return new List<LivroEdicaoLocalVenda>
        {
            GerarObjeto(),
            GerarObjeto(),
            GerarObjeto(),
        };
}
}
