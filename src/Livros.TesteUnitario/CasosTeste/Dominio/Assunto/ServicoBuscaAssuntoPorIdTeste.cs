using Bogus;
using Livros.Dominio.Contratos;
using Livros.Dominio.Servicos.Assunto.BuscarPorId;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoBuscaAssuntoPorIdTeste
{
    private Mock<IRepositorioAssunto> _repositorioAssunto;

    public ServicoBuscaAssuntoPorIdTeste()
    {
        _repositorioAssunto = new();
    }

    private ServicoBuscaAssuntoPorId GerarCenario()
    {
        return new ServicoBuscaAssuntoPorId(_repositorioAssunto.Object);
    }

    [Fact(DisplayName = "Não deve buscar se o id do assunto estiver inválido")]
    public async Task NaoDeveBuscarSeIdAssuntoEstiverInvalido()
    {
        var id = 0;

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.BuscarPorIdAsync(id, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Buscar assunto que não está cadastrado")]
    public async Task BuscarAssuntoQueNaoEstaCadastrado()
    {
        var id = new Faker().Random.Int(min: 1);
        var assuntoNaoEncontrado = AssuntoMock.GerarObjetoNulo();

        _repositorioAssunto.Setup(repository =>
            repository.BuscarPorIdAsync(id))
        .ReturnsAsync(assuntoNaoEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.BuscarPorIdAsync(id, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Buscar assunto que está cadastrado")]
    public async Task BuscarAssuntoQueEstaCadastrado()
    {
        var id = new Faker().Random.Int(min: 1);
        var assuntoEncontrado = AssuntoMock.GerarObjetoValido();

        _repositorioAssunto.Setup(repository =>
            repository.BuscarPorIdAsync(id))
        .ReturnsAsync(assuntoEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.BuscarPorIdAsync(id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(servicoDominio.Notifications);
    }
}