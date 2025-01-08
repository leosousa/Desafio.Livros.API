using AutoMapper;
using Livros.Aplicacao.CasosUso.Autor.Listar;
using Livros.Aplicacao.CasosUso.Livro.Editar;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.Contratos.Servicos.LocalVenda;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using Livros.Dominio.Servicos.Assunto.Listar;
using Livros.Dominio.Servicos.Autor.Listar;
using Livros.Dominio.Servicos.LocalVenda.Listar;
using Livros.TesteUnitario.Mocks;
using Livros.TesteUnitario.Mocks.Aplicacao.Autor;
using Livros.TesteUnitario.Mocks.Aplicacao.Livro;
using Livros.TesteUnitario.Mocks.Dominio.DTOs;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class LivroEdicaoCommandHandlerTeste
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IServicoEdicaoLivro> _servicoEdicaoLivro;
    private readonly Mock<IServicoListagemAutor> _servicoListagemAutor;
    private readonly Mock<IServicoListagemAssunto> _servicoListagemAssunto;
    private readonly Mock<IServicoListagemLocalVenda> _servicoListagemLocalVenda;

    public LivroEdicaoCommandHandlerTeste()
    {
        _mapper = new();
        _servicoEdicaoLivro = new();
        _servicoListagemAutor = new();
        _servicoListagemAssunto = new();
        _servicoListagemLocalVenda = new();
    }

    private LivroEdicaoCommandHandler GerarCenario()
    {
        return new LivroEdicaoCommandHandler(
            _mapper.Object,
            _servicoEdicaoLivro.Object,
            _servicoListagemAutor.Object,
            _servicoListagemAssunto.Object,
            _servicoListagemLocalVenda.Object
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

    [Fact(DisplayName = "Não eve editar o livro se os autores não forem encontrados")]
    public async Task NaoDeveEditarLivroSeNaoEncontraremAutores()
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

        _servicoListagemAutor.Setup(service =>
            service.ListarAsync(It.IsAny<AutorListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(AutorListaPaginadaResultMock.GerarListaSemItens());

        _servicoEdicaoLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoEdicaoLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoEdicaoLivro.SetupGet(property => property.IsValid).Returns(true);

        _mapper.Setup(mapper =>
            mapper.Map<LivroEdicaoCommandResult>(It.IsAny<Livro>()))
        .Returns(livroEdicaoCommandResult);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(EResultadoAcaoServico.NaoEncontrado, result.ResultadoAcao);
        Assert.Contains(result.Notifications, notification => notification.Message == Mensagens.AutorNaoEncontrado);
    }

    [Fact(DisplayName = "Não eve editar o livro se os assuntos não forem encontrados")]
    public async Task NaoDeveEditarLivroSeNaoEncontraremAssuntos()
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

        _servicoListagemAutor.Setup(service =>
            service.ListarAsync(It.IsAny<AutorListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(AutorListaPaginadaResultMock.GerarObjeto());

        _servicoListagemAssunto.Setup(service =>
           service.ListarAsync(It.IsAny<AssuntoListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
       .ReturnsAsync(AssuntoListaPaginadaResultMock.GerarListaSemItens());

        _servicoEdicaoLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoEdicaoLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoEdicaoLivro.SetupGet(property => property.IsValid).Returns(true);

        _mapper.Setup(mapper =>
            mapper.Map<LivroEdicaoCommandResult>(It.IsAny<Livro>()))
        .Returns(livroEdicaoCommandResult);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(EResultadoAcaoServico.NaoEncontrado, result.ResultadoAcao);
        Assert.Contains(result.Notifications, notification => notification.Message == Mensagens.AssuntoNaoEncontrado);
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

        _servicoListagemAutor.Setup(service =>
            service.ListarAsync(It.IsAny<AutorListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(AutorListaPaginadaResultMock.GerarObjeto());

        _servicoListagemAssunto.Setup(service =>
            service.ListarAsync(It.IsAny<AssuntoListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(AssuntoListaPaginadaResultMock.GerarObjeto());

        _servicoListagemLocalVenda.Setup(service =>
            service.ListarAsync(It.IsAny<LocalVendaListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(LocalVendaListaPaginadaResultMock.GerarObjeto());

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