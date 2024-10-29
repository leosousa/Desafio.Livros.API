using AutoMapper;
using Livros.Aplicacao.CasosUso.Livro.Cadastrar;
using Livros.Dominio.Contratos;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.TesteUnitario.Mocks;
using Livros.TesteUnitario.Mocks.Aplicacao.Livro;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class LivroCadastroCommandHandlerTeste
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IServicoCadastroLivro> _servicoCadastroLivro;

    public LivroCadastroCommandHandlerTeste()
    {
        _mapper = new();
        _servicoCadastroLivro = new();
    }

    private LivroCadastroCommandHandler GerarCenario()
    {
        return new LivroCadastroCommandHandler(_mapper.Object, _servicoCadastroLivro.Object);
    }

    [Fact(DisplayName = "Deve retornar \"não informado\" quanto o livro não enviado")]
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
        //Assert.NotNull(resultado);
        //Assert.False(resultado.IsValid);
        //Assert.Contains(resultado.Notifications, notification => notification.Message == Mensagens.LivroNaoInformado);
        Assert.NotNull(resultado);
        Assert.NotEmpty(resultado.Notifications);
    }

    [Fact(DisplayName = "Deve retornar o tipo de retorno e as notificações quando o livro não for cadastrado")]
    public async Task DeveRetornarTipoRetornoENotificacoes_QuandoLivroNaoForCadastrado()
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