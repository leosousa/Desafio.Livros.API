using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Cadastrar;
using Livros.TesteUnitario.Mocks.Aplicacao.Livro.Cadastrar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroCadastroCommandMock : Faker<LivroCadastroCommand>
{
    private LivroCadastroCommandMock() : base("pt_BR")
    {
        RuleFor(autor => autor.Titulo, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Livro.TITULO_MAXIMO_CARACTERES));

        RuleFor(autor => autor.Editora, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Livro.EDITORA_MAXIMO_CARACTERES));

        RuleFor(autor => autor.Edicao, faker => faker.Lorem.Random.Int(min: 1, max: 100));

        RuleFor(autor => autor.AnoPublicacao, faker => faker.Lorem.Random.Int(min: 0, max: DateTime.Now.Year));

        RuleFor(autor => autor.Autores, faker => new List<int> { faker.Lorem.Random.Int(min: 1, max: 100) });

        RuleFor(autor => autor.Assuntos, faker => new List<int> { faker.Lorem.Random.Int(min: 1, max: 100) });

        RuleFor(autor => autor.LocaisVenda, LivroCadastroLocalVendaMock.GerarObjetoLista());
    }

    public static LivroCadastroCommand? GerarObjetoNulo() => null;

    public static LivroCadastroCommand GerarObjetoValido()
    {
        return new LivroCadastroCommandMock().Generate();
    }

    public static LivroCadastroCommand GerarObjetoInvalido()
    {
        var livroInvalido = GerarObjetoValido();

        livroInvalido.Titulo = new Faker().Lorem.Random.String(1, Livros.Dominio.Entidades.Livro.TITULO_MAXIMO_CARACTERES + 1);

        return livroInvalido;
    }
}