using AutoMapper;
using Livros.Aplicacao.CasosUso.Autor.Cadastrar;
using Livros.Dominio.Contratos;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.TesteUnitario.Mocks;
using Livros.TesteUnitario.Mocks.Aplicacao.Autor;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class AutorCadastroCommandHandlerTeste
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IServicoCadastroAutor> _servicoCadastroAutor;

    public AutorCadastroCommandHandlerTeste()
    {
        _mapper = new();
        _servicoCadastroAutor = new();
    }

    private AutorCadastroCommandHandler GerarCenario()
    {
        return new AutorCadastroCommandHandler(_mapper.Object, _servicoCadastroAutor.Object);
    }

    [Fact(DisplayName = "Deve retornar \"não informado\" quanto o autor não enviado")]
    public async Task DeveRetornarNaoInformado_QuandoAutorNaoForEnviado()
    {
        // Arrange
        var autorEnviado = AutorCadastroCommandMock.GerarObjetoNulo();

        _servicoCadastroAutor.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.ParametrosInvalidos);
        _servicoCadastroAutor.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoCadastroAutor.SetupGet(property => property.IsValid).Returns(false);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(autorEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.NotEmpty(resultado.Notifications);
    }

    [Fact(DisplayName = "Deve retornar o tipo de retorno e as notificações quando o autor não for cadastrado")]
    public async Task DeveRetornarTipoRetornoENotificacoes_QuandoAutorNaoForCadastrado()
    {
        // Arrange
        var autorEnviado = AutorCadastroCommandMock.GerarObjetoInvalido();
        var autorInvalido = AutorMock.GerarObjetoInvalido();

        _mapper.Setup(mapper => mapper.Map<Autor>(autorEnviado)).Returns(autorInvalido);

        _servicoCadastroAutor.Setup(
            servicoCadastroAutor => servicoCadastroAutor.CadastrarAsync(It.IsAny<Autor>(), CancellationToken.None)
        ).ReturnsAsync(autorInvalido);

        _servicoCadastroAutor.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Erro);
        _servicoCadastroAutor.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoCadastroAutor.SetupGet(property => property.IsValid).Returns(false);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(autorEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.NotEmpty(resultado.Notifications);
    }

    [Fact(DisplayName = "Deve retornar o autor quando o mesmo for cadastrado com sucesso")]
    public async Task DeveRetornarAutor_QuandoOMesmoForCadastradoComSucesso()
    {
        // Arrange
        var autorEnviado = AutorCadastroCommandMock.GerarObjetoValido();
        var autorValido = AutorMock.GerarObjetoValido();
        var autorCadastrado = AutorCadastroCommandResultMock.GerarObjeto();

        _mapper.Setup(mapper => mapper.Map<Autor>(autorEnviado)).Returns(autorValido);
        _mapper.Setup(mapper => mapper.Map<AutorCadastroCommandResult>(It.IsAny<Autor>())).Returns(autorCadastrado);

        _servicoCadastroAutor.Setup(
            servicoCadastroAutor => servicoCadastroAutor.CadastrarAsync(It.IsAny<Autor>(), CancellationToken.None)
        ).ReturnsAsync(autorValido);

        _servicoCadastroAutor.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoCadastroAutor.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoCadastroAutor.SetupGet(property => property.IsValid).Returns(true);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(autorEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.True(resultado.IsValid);
        Assert.Empty(resultado.Notifications);
    }
}