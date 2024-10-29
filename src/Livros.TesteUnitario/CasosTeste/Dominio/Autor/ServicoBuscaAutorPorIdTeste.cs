using Bogus;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Servicos.Autor.BuscarPorId;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoBuscaAutorPorIdTeste
{
    private Mock<IRepositorioAutor> _repositorioAutor;

    public ServicoBuscaAutorPorIdTeste()
    {
        _repositorioAutor = new();
    }

    private ServicoBuscaAutorPorId GerarCenario()
    {
        return new ServicoBuscaAutorPorId(_repositorioAutor.Object);
    }

    [Fact(DisplayName = "Não deve buscar se o id do autor estiver inválido")]
    public async Task NaoDeveBuscarSeIdAutorEstiverInvalido()
    {
        var id = 0;

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.BuscarPorIdAsync(id, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Buscar autor que não está cadastrado")]
    public async Task BuscarAutorQueNaoEstaCadastrado()
    {
        var id = new Faker().Random.Int(min: 1);
        var autorNaoEncontrado = AutorMock.GerarObjetoNulo();

        _repositorioAutor.Setup(repository =>
            repository.BuscarPorIdAsync(id))
        .ReturnsAsync(autorNaoEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.BuscarPorIdAsync(id, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Buscar autor que está cadastrado")]
    public async Task BuscarAutorQueEstaCadastrado()
    {
        var id = new Faker().Random.Int(min: 1);
        var autorEncontrado = AutorMock.GerarObjetoValido();

        _repositorioAutor.Setup(repository =>
            repository.BuscarPorIdAsync(id))
        .ReturnsAsync(autorEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.BuscarPorIdAsync(id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(servicoDominio.Notifications);
    }
}