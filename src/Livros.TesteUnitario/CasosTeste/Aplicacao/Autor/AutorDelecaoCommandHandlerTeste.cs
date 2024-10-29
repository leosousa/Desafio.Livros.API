using Livros.Aplicacao.CasosUso.Autor.Deletar;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Enumeracoes;
using Livros.TesteUnitario.Mocks;
using Livros.TesteUnitario.Mocks.Aplicacao.Autor.Deletar;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class AutorDelecaoCommandHandlerTeste
{
    private readonly Mock<IServicoDelecaoAutor> _servicoDelecaoAutor;

    public AutorDelecaoCommandHandlerTeste()
    {
        _servicoDelecaoAutor = new();
    }

    private AutorDelecaoCommandHandler GerarCenario()
    {
        return new AutorDelecaoCommandHandler(
            _servicoDelecaoAutor.Object
        );
    }

    [Fact(DisplayName = "Não deve deletar se o identificador do autor não for informado")]
    public async Task NaoDeveBuscarAutorSeIdentificadorNaoForInformado()
    {
        var query = AutorDelecaoCommandMock.GerarObjetoNulo();
        var autor = AutorMock.GerarObjetoInvalido();

        _servicoDelecaoAutor.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.ParametrosInvalidos);
        _servicoDelecaoAutor.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoDelecaoAutor.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve deletar o autor se o mesmo não for encontrado")]
    public async Task NaoDeveDeletarAutorSeOMesmoNaoForEncontrado()
    {
        var command = AutorDelecaoCommandMock.GerarObjeto();
        var autor = AutorMock.GerarObjetoInvalido();

        _servicoDelecaoAutor.Setup(service =>
            service.RemoverAsync(It.IsAny<int>(), CancellationToken.None))
        .ReturnsAsync(false);

        _servicoDelecaoAutor.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.NaoEncontrado);
        _servicoDelecaoAutor.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoDelecaoAutor.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve deletar o autor se houver um erro de infra")]
    public async Task NaoDeveDeletarAutorSeHouverErroInfra()
    {
        var command = AutorDelecaoCommandMock.GerarObjeto();
        var autor = AutorMock.GerarObjetoInvalido();

        _servicoDelecaoAutor.Setup(service =>
            service.RemoverAsync(It.IsAny<int>(), CancellationToken.None))
        .ReturnsAsync(false);

        _servicoDelecaoAutor.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Erro);
        _servicoDelecaoAutor.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoDelecaoAutor.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Deve deletar o autor se os dados forem válidos")]
    public async Task DeveDeletarAutorSeDadosForemValidos()
    {
        var command = AutorDelecaoCommandMock.GerarObjeto();
        var autor = AutorMock.GerarObjetoValido();
        var autorDelecaoCommandResult = AutorDelecaoCommandResultMock.GerarObjeto();

        _servicoDelecaoAutor.Setup(service =>
            service.RemoverAsync(It.IsAny<int>(), CancellationToken.None))
        .ReturnsAsync(true);

        _servicoDelecaoAutor.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoDelecaoAutor.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoDelecaoAutor.SetupGet(property => property.IsValid).Returns(true);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Notifications);
    }
}