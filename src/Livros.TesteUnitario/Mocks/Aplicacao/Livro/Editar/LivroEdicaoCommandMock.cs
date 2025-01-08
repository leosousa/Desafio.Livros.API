using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Editar;
using Livros.TesteUnitario.Mocks.Aplicacao.Livro.Editar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroEdicaoCommandMock : Faker<LivroEdicaoCommand>
{
    private LivroEdicaoCommandMock() : base("pt_BR")
    {
        RuleFor(livro => livro.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(autor => autor.Titulo, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Livro.TITULO_MAXIMO_CARACTERES));

        RuleFor(autor => autor.Editora, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Livro.EDITORA_MAXIMO_CARACTERES));

        RuleFor(autor => autor.Edicao, faker => faker.Lorem.Random.Int(min: 1, max: 100));

        RuleFor(autor => autor.AnoPublicacao, faker => faker.Lorem.Random.Int(min: 0, max: DateTime.Now.Year));

        RuleFor(autor => autor.Autores, faker => new List<int> { faker.Lorem.Random.Int(min: 1, max: 100) });

        RuleFor(autor => autor.Assuntos, faker => new List<int> { faker.Lorem.Random.Int(min: 1, max: 100) });

        RuleFor(autor => autor.LocaisVenda, LivroEdicaoLocalVendaMock.GerarObjetoLista());
    }

    public static LivroEdicaoCommand GerarObjeto()
    {
        return new LivroEdicaoCommandMock().Generate();
    }

    public static LivroEdicaoCommand? GerarObjetoNulo() => null;
}