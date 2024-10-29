using Bogus;
using Livros.Aplicacao.CasosUso.Livro.Listar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Livro;

public class LivroListaPaginadaQueryMock : Faker<LivroListaPaginadaQuery>
{
    private LivroListaPaginadaQueryMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(entidade => entidade.Titulo, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Livro.TITULO_MAXIMO_CARACTERES));

        RuleFor(entidade => entidade.Editora, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Livro.EDITORA_MAXIMO_CARACTERES));

        RuleFor(entidade => entidade.Edicao, faker => faker.Lorem.Random.Int(min: 1, int.MaxValue));

        RuleFor(entidade => entidade.AnoPublicacao, faker => faker.Lorem.Random.Int(min: 1, int.MaxValue));

        RuleFor(entidade => entidade.Assunto, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Assunto.ASSUNTO_DESCRICAO_MAXIMO_CARACTERES));

        RuleFor(entidade => entidade.Autor, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Autor.AUTOR_NOME_MAXIMO_CARACTERES));
    }

    public static LivroListaPaginadaQuery GerarObjeto()
    {
        return new LivroListaPaginadaQueryMock().Generate();
    }

    public static LivroListaPaginadaQuery? GerarObjetoNulo() => null;
}