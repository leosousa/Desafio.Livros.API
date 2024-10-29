using Livros.Aplicacao.CasosUso.Assunto.Deletar;
using Livros.Aplicacao.CasosUso.Assunto.Editar;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Servicos.Assunto.Editar;
using Livros.TesteUnitario.Mocks;
using Livros.TesteUnitario.Mocks.Aplicacao.Assunto;
using Livros.TesteUnitario.Mocks.Aplicacao.Assunto.Deletar;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class AssuntoDelecaoCommandHandlerTeste
{
    private readonly Mock<IServicoDelecaoAssunto> _servicoDelecaoAssunto;

    public AssuntoDelecaoCommandHandlerTeste()
    {
        _servicoDelecaoAssunto = new();
    }

    private AssuntoDelecaoCommandHandler GerarCenario()
    {
        return new AssuntoDelecaoCommandHandler(
            _servicoDelecaoAssunto.Object
        );
    }

    [Fact(DisplayName = "Não deve deletar se o identificador do assunto não for informado")]
    public async Task NaoDeveBuscarAssuntoSeIdentificadorNaoForInformado()
    {
        var query = AssuntoDelecaoCommandMock.GerarObjetoNulo();
        var assunto = AssuntoMock.GerarObjetoInvalido();

        _servicoDelecaoAssunto.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.ParametrosInvalidos);
        _servicoDelecaoAssunto.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoDelecaoAssunto.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve deletar o assunto se o mesmo não for encontrado")]
    public async Task NaoDeveDeletarAssuntoSeOMesmoNaoForEncontrado()
    {
        var command = AssuntoDelecaoCommandMock.GerarObjeto();
        var assunto = AssuntoMock.GerarObjetoInvalido();

        _servicoDelecaoAssunto.Setup(service =>
            service.RemoverAsync(It.IsAny<int>(), CancellationToken.None))
        .ReturnsAsync(false);

        _servicoDelecaoAssunto.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.NaoEncontrado);
        _servicoDelecaoAssunto.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoDelecaoAssunto.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve deletar o assunto se houver um erro de infra")]
    public async Task NaoDeveDeletarAssuntoSeHouverErroInfra()
    {
        var command = AssuntoDelecaoCommandMock.GerarObjeto();
        var assunto = AssuntoMock.GerarObjetoInvalido();

        _servicoDelecaoAssunto.Setup(service =>
            service.RemoverAsync(It.IsAny<int>(), CancellationToken.None))
        .ReturnsAsync(false);

        _servicoDelecaoAssunto.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Erro);
        _servicoDelecaoAssunto.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoDelecaoAssunto.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Deve deletar o assunto se os dados forem válidos")]
    public async Task DeveDeletarAssuntoSeDadosForemValidos()
    {
        var command = AssuntoDelecaoCommandMock.GerarObjeto();
        var assunto = AssuntoMock.GerarObjetoValido();
        var assuntoDelecaoCommandResult = AssuntoDelecaoCommandResultMock.GerarObjeto();

        _servicoDelecaoAssunto.Setup(service =>
            service.RemoverAsync(It.IsAny<int>(), CancellationToken.None))
        .ReturnsAsync(true);

        _servicoDelecaoAssunto.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoDelecaoAssunto.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoDelecaoAssunto.SetupGet(property => property.IsValid).Returns(true);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Notifications);
    }
}