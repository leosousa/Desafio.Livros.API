using Bogus;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Servicos.Autor.Deletar;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoDelecaoAutorTest
{
    private Mock<IRepositorioAutor> _repositorioAutor;

    public ServicoDelecaoAutorTest()
    {
        _repositorioAutor = new();
    }

    private ServicoDelecaoAutor GerarCenario()
    {
        return new ServicoDelecaoAutor(_repositorioAutor.Object);
    }

    [Fact(DisplayName = "Não deve deletar o autor se o id for maior ou igual a 0")]
    public async Task NaoDeveDeletarAutorSeOMesmoNaoForEnviado()
    {
        var id = new Faker().Random.Int(min: int.MinValue, max: 0);

        var servicoDominio = GerarCenario();

        var autorNaoRemovido = await servicoDominio.RemoverAsync(id!, CancellationToken.None);

        Assert.False(autorNaoRemovido);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve deletar o autor se o mesmo não for encontrado")]
    public async Task NaoDeveDeletarAutorSeOMesmoNaoForEncontrado()
    {
        var id = new Faker().Random.Int(min: 1, max: 100);

        var autorNaoEncontrado = AutorMock.GerarObjetoNulo();

        _repositorioAutor.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(autorNaoEncontrado);

        var servicoDominio = GerarCenario();

        var autorNaoRemovido = await servicoDominio.RemoverAsync(id!, CancellationToken.None);

        Assert.False(autorNaoRemovido);
        Assert.Equal(EResultadoAcaoServico.NaoEncontrado, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não Deve deletar o autor em caso de erro de infra")]
    public async Task NaoDeveDeletarAutorSeHouverErroInfra()
    {
        var id = new Faker().Random.Int(min: 1, max: 100);
        var autorEncontrado = AutorMock.GerarObjetoValido();
        var autorDeletado = false;

        _repositorioAutor.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(autorEncontrado);

        _repositorioAutor.Setup(repositorio =>
           repositorio.RemoverAsync(It.IsAny<Autor>()))
       .ReturnsAsync(autorDeletado!);

        var servicoDominio = GerarCenario();

        var autorNaoRemovido = await servicoDominio.RemoverAsync(id!, CancellationToken.None);

        Assert.False(autorNaoRemovido);
        Assert.Equal(EResultadoAcaoServico.Erro, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve deletar o autor se o id informado está corretos")]
    public async Task DeveDeletarAutorSeIdInformadoEstaValido()
    {
        var id = new Faker().Random.Int(min: 1, max: 100);
        var autorEncontrado = AutorMock.GerarObjetoValido();
        var autorDeletado = true;

        _repositorioAutor.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(autorEncontrado);

        _repositorioAutor.Setup(repositorio =>
           repositorio.RemoverAsync(It.IsAny<Autor>()))
       .ReturnsAsync(autorDeletado!);

        var servicoDominio = GerarCenario();

        var autorNaoRemovido = await servicoDominio.RemoverAsync(id!, CancellationToken.None);

        Assert.True(autorNaoRemovido);
        Assert.Equal(EResultadoAcaoServico.Suceso, servicoDominio.ResultadoAcao);
        Assert.Empty(servicoDominio.Notifications);
    }
}