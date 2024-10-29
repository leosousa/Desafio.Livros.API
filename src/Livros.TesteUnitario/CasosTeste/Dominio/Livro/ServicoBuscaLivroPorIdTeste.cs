using Bogus;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Servicos.Livro.BuscarPorId;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoBuscaLivroPorIdTeste
{
    private Mock<IRepositorioLivro> _repositorioLivro;

    public ServicoBuscaLivroPorIdTeste()
    {
        _repositorioLivro = new();
    }

    private ServicoBuscaLivroPorId GerarCenario()
    {
        return new ServicoBuscaLivroPorId(_repositorioLivro.Object);
    }

    [Fact(DisplayName = "Não deve buscar se o id do livro estiver inválido")]
    public async Task NaoDeveBuscarSeIdLivroEstiverInvalido()
    {
        var id = 0;

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.BuscarPorIdAsync(id, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Buscar livro que não está cadastrado")]
    public async Task BuscarLivroQueNaoEstaCadastrado()
    {
        var id = new Faker().Random.Int(min: 1);
        var livroNaoEncontrado = LivroMock.GerarObjetoNulo();

        _repositorioLivro.Setup(repository =>
            repository.BuscarPorIdAsync(id))
        .ReturnsAsync(livroNaoEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.BuscarPorIdAsync(id, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Buscar livro que está cadastrado")]
    public async Task BuscarLivroQueEstaCadastrado()
    {
        var id = new Faker().Random.Int(min: 1);
        var livroEncontrado = LivroMock.GerarObjetoValido();

        _repositorioLivro.Setup(repository =>
            repository.BuscarPorIdAsync(id))
        .ReturnsAsync(livroEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.BuscarPorIdAsync(id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(servicoDominio.Notifications);
    }
}