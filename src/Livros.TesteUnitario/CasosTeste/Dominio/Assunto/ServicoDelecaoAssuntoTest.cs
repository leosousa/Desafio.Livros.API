using Bogus;
using Livros.Dominio.Contratos;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Servicos.Assunto.Deletar;
using Livros.Dominio.Servicos.Assunto.Editar;
using Livros.TesteUnitario.Mocks.Dominio;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoDelecaoAssuntoTest
{
    private Mock<IRepositorioAssunto> _repositorioAssunto;

    public ServicoDelecaoAssuntoTest()
    {
        _repositorioAssunto = new();
    }

    private ServicoDelecaoAssunto GerarCenario()
    {
        return new ServicoDelecaoAssunto(_repositorioAssunto.Object);
    }

    [Fact(DisplayName = "Não deve deletar o assunto se o id for maior ou igual a 0")]
    public async Task NaoDeveDeletarAssuntoSeOMesmoNaoForEnviado()
    {
        var id = new Faker().Random.Int(min: int.MinValue, max: 0);

        var servicoDominio = GerarCenario();

        var assuntoNaoRemovido = await servicoDominio.RemoverAsync(id!, CancellationToken.None);

        Assert.False(assuntoNaoRemovido);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve deletar o assunto se o mesmo não for encontrado")]
    public async Task NaoDeveDeletarAssuntoSeOMesmoNaoForEncontrado()
    {
        var id = new Faker().Random.Int(min: 1, max: 100);

        var assuntoNaoEncontrado = AssuntoMock.GerarObjetoNulo();

        _repositorioAssunto.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(assuntoNaoEncontrado);

        var servicoDominio = GerarCenario();

        var assuntoNaoRemovido = await servicoDominio.RemoverAsync(id!, CancellationToken.None);

        Assert.False(assuntoNaoRemovido);
        Assert.Equal(EResultadoAcaoServico.NaoEncontrado, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não Deve deletar o assunto em caso de erro de infra")]
    public async Task NaoDeveDeletarAssuntoSeHouverErroInfra()
    {
        var id = new Faker().Random.Int(min: 1, max: 100);
        var assuntoEncontrado = AssuntoMock.GerarObjetoValido();
        var assuntoDeletado = false;

        _repositorioAssunto.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(assuntoEncontrado);

        _repositorioAssunto.Setup(repositorio =>
           repositorio.RemoverAsync(It.IsAny<Assunto>()))
       .ReturnsAsync(assuntoDeletado!);

        var servicoDominio = GerarCenario();

        var assuntoNaoRemovido = await servicoDominio.RemoverAsync(id!, CancellationToken.None);

        Assert.False(assuntoNaoRemovido);
        Assert.Equal(EResultadoAcaoServico.Erro, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve deletar o assunto se o id informado está corretos")]
    public async Task DeveDeletarAssuntoSeIdInformadoEstaValido()
    {
        var id = new Faker().Random.Int(min: 1, max: 100);
        var assuntoEncontrado = AssuntoMock.GerarObjetoValido();
        var assuntoDeletado = true;

        _repositorioAssunto.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(assuntoEncontrado);

        _repositorioAssunto.Setup(repositorio =>
           repositorio.RemoverAsync(It.IsAny<Assunto>()))
       .ReturnsAsync(assuntoDeletado!);

        var servicoDominio = GerarCenario();

        var assuntoNaoRemovido = await servicoDominio.RemoverAsync(id!, CancellationToken.None);

        Assert.True(assuntoNaoRemovido);
        Assert.Equal(EResultadoAcaoServico.Suceso, servicoDominio.ResultadoAcao);
        Assert.Empty(servicoDominio.Notifications);
    }
}