using AutoMapper;
using Livros.Aplicacao.CasosUso.Assunto.Editar;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.TesteUnitario.Mocks;
using Livros.TesteUnitario.Mocks.Aplicacao.Assunto;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class AssuntoEdicaoCommandHandlerTeste
{
    private readonly Mock<IServicoEdicaoAssunto> _servicoEdicaoAssunto;
    private readonly Mock<IMapper> _mapper;

    public AssuntoEdicaoCommandHandlerTeste()
    {
        _servicoEdicaoAssunto = new();
        _mapper = new();
    }

    private AssuntoEdicaoCommandHandler GerarCenario()
    {
        return new AssuntoEdicaoCommandHandler(
            _servicoEdicaoAssunto.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Não deve editar o assunto se o mesmo for inválido")]
    public async Task NaoDeveEditarAssuntoSeOMesmoForInvalido()
    {
        var command = AssuntoEdicaoCommandMock.GerarObjeto();
        var assunto = AssuntoMock.GerarObjetoInvalido();

        _mapper.Setup(mapper =>
            mapper.Map<Assunto>(It.IsAny<AssuntoEdicaoCommand>()))
        .Returns(assunto);

        _servicoEdicaoAssunto.Setup(service =>
            service.EditarAsync(It.IsAny<Assunto>(), CancellationToken.None))
        .ReturnsAsync(assunto);

        _servicoEdicaoAssunto.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.ParametrosInvalidos);
        _servicoEdicaoAssunto.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoEdicaoAssunto.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o assunto se o mesmo não for encontrado")]
    public async Task NaoDeveEditarAssuntoSeOMesmoNaoForEncontrado()
    {
        var command = AssuntoEdicaoCommandMock.GerarObjeto();
        var assunto = AssuntoMock.GerarObjetoInvalido();

        _mapper.Setup(mapper =>
            mapper.Map<Assunto>(It.IsAny<AssuntoEdicaoCommand>()))
        .Returns(assunto);

        _servicoEdicaoAssunto.Setup(service =>
            service.EditarAsync(It.IsAny<Assunto>(), CancellationToken.None))
        .ReturnsAsync(assunto);

        _servicoEdicaoAssunto.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.NaoEncontrado);
        _servicoEdicaoAssunto.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoEdicaoAssunto.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o assunto se houver um erro de infra")]
    public async Task NaoDeveEditarAssuntoSeHouverErroInfra()
    {
        var command = AssuntoEdicaoCommandMock.GerarObjeto();
        var assunto = AssuntoMock.GerarObjetoInvalido();

        _mapper.Setup(mapper =>
            mapper.Map<Assunto>(It.IsAny<AssuntoEdicaoCommand>()))
        .Returns(assunto);

        _servicoEdicaoAssunto.Setup(service =>
            service.EditarAsync(It.IsAny<Assunto>(), CancellationToken.None))
        .ReturnsAsync(assunto);

        _servicoEdicaoAssunto.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Erro);
        _servicoEdicaoAssunto.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoEdicaoAssunto.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Deve editar o assunto se os dados forem válidos")]
    public async Task DeveEditarAssuntoSeDadosForemValidos()
    {
        var command = AssuntoEdicaoCommandMock.GerarObjeto();
        var assunto = AssuntoMock.GerarObjetoValido();
        var assuntoEdicaoCommandResult = AssuntoEdicaoCommandResultMock.GerarObjeto();

        _mapper.Setup(mapper =>
            mapper.Map<Assunto>(It.IsAny<AssuntoEdicaoCommand>()))
        .Returns(assunto);

        _servicoEdicaoAssunto.Setup(service =>
            service.EditarAsync(It.IsAny<Assunto>(), CancellationToken.None))
        .ReturnsAsync(assunto);

        _servicoEdicaoAssunto.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoEdicaoAssunto.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoEdicaoAssunto.SetupGet(property => property.IsValid).Returns(true);

        _mapper.Setup(mapper =>
            mapper.Map<AssuntoEdicaoCommandResult>(It.IsAny<Assunto>()))
        .Returns(assuntoEdicaoCommandResult);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Notifications);
    }
}