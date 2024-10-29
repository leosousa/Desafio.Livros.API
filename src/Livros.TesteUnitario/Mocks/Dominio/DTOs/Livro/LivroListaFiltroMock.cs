using Bogus;
using Livros.Dominio.Entidades;
using Livros.Dominio.Servicos.Livro.Listar;

namespace Livros.TesteUnitario.Mocks.Dominio.DTOs;

public class LivroListaFiltroMock : Faker<LivroListaFiltro>
{
    private LivroListaFiltroMock() : base("pt_BR")
    {
        RuleFor(livro => livro.Titulo, faker => faker.Lorem.Random.String(1, Livro.TITULO_MAXIMO_CARACTERES));

        RuleFor(livro => livro.Editora, faker => faker.Lorem.Random.String(1, Livro.EDITORA_MAXIMO_CARACTERES));

        RuleFor(livro => livro.Edicao, faker => faker.Lorem.Random.Int(min: 1, max: 100));

        RuleFor(livros => livros.AnoPublicacao, faker => faker.Lorem.Random.Int(min: 0, max: DateTime.Now.Year));

        RuleFor(livros => livros.Autor, faker => faker.Lorem.Random.String(1, Autor.AUTOR_NOME_MAXIMO_CARACTERES));

        RuleFor(livros => livros.Assunto, faker => faker.Lorem.Random.String(1, Assunto.ASSUNTO_DESCRICAO_MAXIMO_CARACTERES));
    }

    public static LivroListaFiltro GerarObjeto()
    {
        return new LivroListaFiltroMock().Generate();
    }

    public static LivroListaFiltro? GerarObjetoNulo() => null;
}