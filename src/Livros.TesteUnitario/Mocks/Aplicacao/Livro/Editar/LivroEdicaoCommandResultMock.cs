using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Editar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroEdicaoCommandResultMock : Faker<LivroEdicaoCommandResult>
{
    private LivroEdicaoCommandResultMock() : base("pt_BR")
    {
        RuleFor(livro => livro.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(livro => livro.Titulo, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Livro.TITULO_MAXIMO_CARACTERES));

        RuleFor(livro => livro.Editora, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Livro.EDITORA_MAXIMO_CARACTERES));

        RuleFor(livro => livro.Edicao, faker => faker.Lorem.Random.Int(min: 1, max: 100));

        RuleFor(livro => livro.AnoPublicacao, faker => faker.Lorem.Random.Int(min: 0, max: DateTime.Now.Year));
    }

    public static LivroEdicaoCommandResult GerarObjeto()
    {
        return new LivroEdicaoCommandResultMock().Generate();
    }
}