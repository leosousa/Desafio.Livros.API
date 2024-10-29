using AutoMapper;
using Livros.Aplicacao.CasosUso.Livro.Editar;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.TesteUnitario.Mocks;
using Livros.TesteUnitario.Mocks.Aplicacao.Livro;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class LivroEdicaoCommandHandlerTeste
{
    private readonly Mock<IServicoEdicaoLivro> _servicoEdicaoLivro;
    private readonly Mock<IMapper> _mapper;

    public LivroEdicaoCommandHandlerTeste()
    {
        _servicoEdicaoLivro = new();
        _mapper = new();
    }

    private LivroEdicaoCommandHandler GerarCenario()
    {
        return new LivroEdicaoCommandHandler(
            _servicoEdicaoLivro.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Não deve editar o livro se o mesmo for inválido")]
    public async Task NaoDeveEditarLivroSeOMesmoForInvalido()
    {
        var command = LivroEdicaoCommandMock.GerarObjeto();
        var livro = LivroMock.GerarObjeto();

        _mapper.Setup(mapper =>
            mapper.Map<Livro>(It.IsAny<LivroEdicaoCommand>()))
        .Returns(livro);

        _servicoEdicaoLivro.Setup(service =>
            service.EditarAsync(It.IsAny<Livro>(), CancellationToken.None))
        .ReturnsAsync(livro);

        _servicoEdicaoLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.ParametrosInvalidos);
        _servicoEdicaoLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoEdicaoLivro.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o livro se o mesmo não for encontrado")]
    public async Task NaoDeveEditarLivroSeOMesmoNaoForEncontrado()
    {
        var command = LivroEdicaoCommandMock.GerarObjeto();
        var livro = LivroMock.GerarObjeto();

        _mapper.Setup(mapper =>
            mapper.Map<Livro>(It.IsAny<LivroEdicaoCommand>()))
        .Returns(livro);

        _servicoEdicaoLivro.Setup(service =>
            service.EditarAsync(It.IsAny<Livro>(), CancellationToken.None))
        .ReturnsAsync(livro);

        _servicoEdicaoLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.NaoEncontrado);
        _servicoEdicaoLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoEdicaoLivro.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o livro se houver um erro de infra")]
    public async Task NaoDeveEditarLivroSeHouverErroInfra()
    {
        var command = LivroEdicaoCommandMock.GerarObjeto();
        var livro = LivroMock.GerarObjeto();

        _mapper.Setup(mapper =>
            mapper.Map<Livro>(It.IsAny<LivroEdicaoCommand>()))
        .Returns(livro);

        _servicoEdicaoLivro.Setup(service =>
            service.EditarAsync(It.IsAny<Livro>(), CancellationToken.None))
        .ReturnsAsync(livro);

        _servicoEdicaoLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Erro);
        _servicoEdicaoLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoEdicaoLivro.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Deve editar o livro se os dados forem válidos")]
    public async Task DeveEditarLivroSeDadosForemValidos()
    {
        var command = LivroEdicaoCommandMock.GerarObjeto();
        var livro = LivroMock.GerarObjetoValido();
        var livroEdicaoCommandResult = LivroEdicaoCommandResultMock.GerarObjeto();

        _mapper.Setup(mapper =>
            mapper.Map<Livro>(It.IsAny<LivroEdicaoCommand>()))
        .Returns(livro);

        _servicoEdicaoLivro.Setup(service =>
            service.EditarAsync(It.IsAny<Livro>(), CancellationToken.None))
        .ReturnsAsync(livro);

        _servicoEdicaoLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoEdicaoLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoEdicaoLivro.SetupGet(property => property.IsValid).Returns(true);

        _mapper.Setup(mapper =>
            mapper.Map<LivroEdicaoCommandResult>(It.IsAny<Livro>()))
        .Returns(livroEdicaoCommandResult);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Notifications);
    }
}