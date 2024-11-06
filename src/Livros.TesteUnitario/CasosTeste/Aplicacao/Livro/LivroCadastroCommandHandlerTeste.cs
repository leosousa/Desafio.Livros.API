using AutoMapper;
using Livros.Aplicacao.CasosUso.Livro.Cadastrar;
using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using Livros.Dominio.Servicos.Assunto.Listar;
using Livros.Dominio.Servicos.Autor.Listar;
using Livros.TesteUnitario.Mocks;
using Livros.TesteUnitario.Mocks.Aplicacao.Livro;
using Livros.TesteUnitario.Mocks.Dominio.DTOs;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class LivroCadastroCommandHandlerTeste
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IServicoCadastroLivro> _servicoCadastroLivro;
    private readonly Mock<IServicoListagemAutor> _servicoListagemAutor;
    private readonly Mock<IServicoListagemAssunto> _servicoListagemAssunto;

    public LivroCadastroCommandHandlerTeste()
    {
        _mapper = new();
        _servicoCadastroLivro = new();
        _servicoListagemAutor = new();
        _servicoListagemAssunto = new();
    }

    private LivroCadastroCommandHandler GerarCenario()
    {
        return new LivroCadastroCommandHandler(_mapper.Object, _servicoCadastroLivro.Object, _servicoListagemAutor.Object, _servicoListagemAssunto.Object);
    }

    [Fact(DisplayName = "Deve retornar 'Parametros Invalidos' quanto o livro não enviado")]
    public async Task DeveRetornarNaoInformado_QuandoLivroNaoForEnviado()
    {
        // Arrange
        var livroEnviado = LivroCadastroCommandMock.GerarObjetoNulo();

        _servicoCadastroLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.ParametrosInvalidos);
        _servicoCadastroLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoCadastroLivro.SetupGet(property => property.IsValid).Returns(false);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(livroEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, resultado.ResultadoAcao);
        Assert.Contains(resultado.Notifications, notification => notification.Message == Mensagens.LivroNaoInformado);
    }

    [Fact(DisplayName = "Deve retornar 'Parametros Invalidos' quanto nenhum autor for enviado")]
    public async Task DeveRetornarParametrosInvalidos_QuandoNenhumAutorForEnviado()
    {
        // Arrange
        var livroEnviado = LivroCadastroCommandMock.GerarObjetoValido();
        livroEnviado.Autores = null;

        _servicoCadastroLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.ParametrosInvalidos);
        _servicoCadastroLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoCadastroLivro.SetupGet(property => property.IsValid).Returns(false);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(livroEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, resultado.ResultadoAcao);
        Assert.Contains(resultado.Notifications, notification => notification.Message == Mensagens.AutorNaoInformado);
    }

    [Fact(DisplayName = "Deve retornar 'Parametros Invalidos' quanto nenhum assunto for enviado")]
    public async Task DeveRetornarParametrosInvalidos_QuandoNenhumAssuntoForEnviado()
    {
        // Arrange
        var livroEnviado = LivroCadastroCommandMock.GerarObjetoValido();
        livroEnviado.Assuntos = null;

        _servicoCadastroLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.ParametrosInvalidos);
        _servicoCadastroLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoCadastroLivro.SetupGet(property => property.IsValid).Returns(false);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(livroEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, resultado.ResultadoAcao);
        Assert.Contains(resultado.Notifications, notification => notification.Message == Mensagens.AssuntoNaoInformado);
    }

    [Fact(DisplayName = "Deve retornar 'Nao encontrado' quando nenhum autor for encontrado")]
    public async Task DeveRetornarNaoEncontrado_QuandoNenhumAutorForEncontrado()
    {
        // Arrange
        var livroEnviado = LivroCadastroCommandMock.GerarObjetoValido();
        var livroValido = LivroMock.GerarObjetoValido();
        var livroCadastrado = LivroCadastroCommandResultMock.GerarObjeto();

        _mapper.Setup(mapper => mapper.Map<Livro>(livroEnviado)).Returns(livroValido);
        _mapper.Setup(mapper => mapper.Map<LivroCadastroCommandResult>(It.IsAny<Livro>())).Returns(livroCadastrado);

        _servicoListagemAutor.Setup(service =>
            service.ListarAsync(It.IsAny<AutorListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(AutorListaPaginadaResultMock.GerarListaSemItens());

        _servicoCadastroLivro.Setup(
            servicoCadastroLivro => servicoCadastroLivro.CadastrarAsync(It.IsAny<Livro>(), CancellationToken.None)
        ).ReturnsAsync(livroValido);

        _servicoCadastroLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoCadastroLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoCadastroLivro.SetupGet(property => property.IsValid).Returns(true);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(livroEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(EResultadoAcaoServico.NaoEncontrado, resultado.ResultadoAcao);
        Assert.Contains(resultado.Notifications, notification => notification.Message == Mensagens.AutorNaoEncontrado);
    }

    [Fact(DisplayName = "Deve retornar 'Nao encontrado' quando nenhum assunto for encontrado")]
    public async Task DeveRetornarNaoEncontrado_QuandoNenhumAssuntoForEncontrado()
    {
        // Arrange
        var livroEnviado = LivroCadastroCommandMock.GerarObjetoValido();
        var livroValido = LivroMock.GerarObjetoValido();
        var livroCadastrado = LivroCadastroCommandResultMock.GerarObjeto();

        _mapper.Setup(mapper => mapper.Map<Livro>(livroEnviado)).Returns(livroValido);
        _mapper.Setup(mapper => mapper.Map<LivroCadastroCommandResult>(It.IsAny<Livro>())).Returns(livroCadastrado);

        _servicoListagemAutor.Setup(service =>
            service.ListarAsync(It.IsAny<AutorListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(AutorListaPaginadaResultMock.GerarObjeto());

        _servicoListagemAssunto.Setup(service =>
            service.ListarAsync(It.IsAny<AssuntoListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(AssuntoListaPaginadaResultMock.GerarListaSemItens());

        _servicoCadastroLivro.Setup(
            servicoCadastroLivro => servicoCadastroLivro.CadastrarAsync(It.IsAny<Livro>(), CancellationToken.None)
        ).ReturnsAsync(livroValido);

        _servicoCadastroLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoCadastroLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoCadastroLivro.SetupGet(property => property.IsValid).Returns(true);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(livroEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(EResultadoAcaoServico.NaoEncontrado, resultado.ResultadoAcao);
        Assert.Contains(resultado.Notifications, notification => notification.Message == Mensagens.AssuntoNaoEncontrado);
    }

    [Fact(DisplayName = "Deve retornar 'Erro' quando o livro não for cadastrado")]
    public async Task DeveRetornarErro_QuandoLivroNaoForCadastrado()
    {
        // Arrange
        var livroEnviado = LivroCadastroCommandMock.GerarObjetoInvalido();
        var livroInvalido = LivroMock.GerarObjeto();

        _mapper.Setup(mapper => mapper.Map<Livro>(livroEnviado)).Returns(livroInvalido);

        _servicoCadastroLivro.Setup(
            servicoCadastroLivro => servicoCadastroLivro.CadastrarAsync(It.IsAny<Livro>(), CancellationToken.None)
        ).ReturnsAsync(livroInvalido);

        _servicoCadastroLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Erro);
        _servicoCadastroLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoCadastroLivro.SetupGet(property => property.IsValid).Returns(false);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(livroEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.NotEmpty(resultado.Notifications);
    }

    [Fact(DisplayName = "Deve retornar o livro quando o mesmo for cadastrado com sucesso")]
    public async Task DeveRetornarLivro_QuandoOMesmoForCadastradoComSucesso()
    {
        // Arrange
        var livroEnviado = LivroCadastroCommandMock.GerarObjetoValido();
        var livroValido = LivroMock.GerarObjetoValido();
        var livroCadastrado = LivroCadastroCommandResultMock.GerarObjeto();

        _mapper.Setup(mapper => mapper.Map<Livro>(livroEnviado)).Returns(livroValido);
        _mapper.Setup(mapper => mapper.Map<LivroCadastroCommandResult>(It.IsAny<Livro>())).Returns(livroCadastrado);

        _servicoListagemAutor.Setup(service =>
            service.ListarAsync(It.IsAny<AutorListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(AutorListaPaginadaResultMock.GerarObjeto());

        _servicoListagemAssunto.Setup(service =>
            service.ListarAsync(It.IsAny<AssuntoListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(AssuntoListaPaginadaResultMock.GerarObjeto());

        _servicoCadastroLivro.Setup(
            servicoCadastroLivro => servicoCadastroLivro.CadastrarAsync(It.IsAny<Livro>(), CancellationToken.None)
        ).ReturnsAsync(livroValido);

        _servicoCadastroLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoCadastroLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoCadastroLivro.SetupGet(property => property.IsValid).Returns(true);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(livroEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.True(resultado.IsValid);
        Assert.Empty(resultado.Notifications);
    }
}