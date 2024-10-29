using Bogus;
using Livros.Aplicacao.CasosUso.Autor.Cadastrar;

namespace Livros.TesteUnitario.Mocks.Aplicacao.Autor;

public class AutorCadastroCommandMock : Faker<AutorCadastroCommand>
{
    private AutorCadastroCommandMock() : base("pt_BR")
    {
        RuleFor(autor => autor.Nome, faker => faker.Lorem.Random.String(1, Livros.Dominio.Entidades.Autor.AUTOR_NOME_MAXIMO_CARACTERES));
    }

    public static AutorCadastroCommand? GerarObjetoNulo() => null;

    public static AutorCadastroCommand GerarObjetoValido()
    {
        return new AutorCadastroCommandMock().Generate();
    }

    public static AutorCadastroCommand GerarObjetoInvalido()
    {
        var autorInvalido = GerarObjetoValido();

        autorInvalido.Nome = new Faker().Lorem.Random.String(21, 100);

        return autorInvalido;
    }
}