using AutoMapper;
using Livros.Aplicacao.CasosUso.Autor.Editar;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.TesteUnitario.Mocks;
using Livros.TesteUnitario.Mocks.Aplicacao.Autor;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class AutorEdicaoCommandHandlerTeste
{
    private readonly Mock<IServicoEdicaoAutor> _servicoEdicaoAutor;
    private readonly Mock<IMapper> _mapper;

    public AutorEdicaoCommandHandlerTeste()
    {
        _servicoEdicaoAutor = new();
        _mapper = new();
    }

    private AutorEdicaoCommandHandler GerarCenario()
    {
        return new AutorEdicaoCommandHandler(
            _servicoEdicaoAutor.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Não deve editar o autor se o mesmo for inválido")]
    public async Task NaoDeveEditarAutorSeOMesmoForInvalido()
    {
        var command = AutorEdicaoCommandMock.GerarObjeto();
        var autor = AutorMock.GerarObjetoInvalido();

        _mapper.Setup(mapper =>
            mapper.Map<Autor>(It.IsAny<AutorEdicaoCommand>()))
        .Returns(autor);

        _servicoEdicaoAutor.Setup(service =>
            service.EditarAsync(It.IsAny<Autor>(), CancellationToken.None))
        .ReturnsAsync(autor);

        _servicoEdicaoAutor.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.ParametrosInvalidos);
        _servicoEdicaoAutor.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoEdicaoAutor.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o autor se o mesmo não for encontrado")]
    public async Task NaoDeveEditarAutorSeOMesmoNaoForEncontrado()
    {
        var command = AutorEdicaoCommandMock.GerarObjeto();
        var autor = AutorMock.GerarObjetoInvalido();

        _mapper.Setup(mapper =>
            mapper.Map<Autor>(It.IsAny<AutorEdicaoCommand>()))
        .Returns(autor);

        _servicoEdicaoAutor.Setup(service =>
            service.EditarAsync(It.IsAny<Autor>(), CancellationToken.None))
        .ReturnsAsync(autor);

        _servicoEdicaoAutor.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.NaoEncontrado);
        _servicoEdicaoAutor.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoEdicaoAutor.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o autor se houver um erro de infra")]
    public async Task NaoDeveEditarAutorSeHouverErroInfra()
    {
        var command = AutorEdicaoCommandMock.GerarObjeto();
        var autor = AutorMock.GerarObjetoInvalido();

        _mapper.Setup(mapper =>
            mapper.Map<Autor>(It.IsAny<AutorEdicaoCommand>()))
        .Returns(autor);

        _servicoEdicaoAutor.Setup(service =>
            service.EditarAsync(It.IsAny<Autor>(), CancellationToken.None))
        .ReturnsAsync(autor);

        _servicoEdicaoAutor.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Erro);
        _servicoEdicaoAutor.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoEdicaoAutor.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Deve editar o autor se os dados forem válidos")]
    public async Task DeveEditarAutorSeDadosForemValidos()
    {
        var command = AutorEdicaoCommandMock.GerarObjeto();
        var autor = AutorMock.GerarObjetoValido();
        var autorEdicaoCommandResult = AutorEdicaoCommandResultMock.GerarObjeto();

        _mapper.Setup(mapper =>
            mapper.Map<Autor>(It.IsAny<AutorEdicaoCommand>()))
        .Returns(autor);

        _servicoEdicaoAutor.Setup(service =>
            service.EditarAsync(It.IsAny<Autor>(), CancellationToken.None))
        .ReturnsAsync(autor);

        _servicoEdicaoAutor.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoEdicaoAutor.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoEdicaoAutor.SetupGet(property => property.IsValid).Returns(true);

        _mapper.Setup(mapper =>
            mapper.Map<AutorEdicaoCommandResult>(It.IsAny<Autor>()))
        .Returns(autorEdicaoCommandResult);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Notifications);
    }
}