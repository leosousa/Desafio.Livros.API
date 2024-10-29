using FluentValidation;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Servicos.Autor.Cadastrar;
using Livros.TesteUnitario.Mocks.Dominio;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoCadastroAutorTeste
{
    private readonly Mock<IValidator<Autor>> _validator;
    private readonly Mock<IRepositorioAutor> _repositorioAutor;

    public ServicoCadastroAutorTeste()
    {
        _validator = new();
        _repositorioAutor = new();
    }

    private ServicoCadastroAutor GerarCenario()
    {
        return new ServicoCadastroAutor(_repositorioAutor.Object, _validator.Object);
    }

    [Fact(DisplayName = "Deve retornar \"não informado\" quanto o autor não for enviado")]
    public async Task DeveRetornarNaoInformado_QuandoAutorForNulo()
    {
        // Arrange
        var autorEnviado = AutorMock.GerarObjetoNulo();

        var servicoDominio = GerarCenario();

        // Act
        var resultado = await servicoDominio.CadastrarAsync(autorEnviado!, CancellationToken.None);

        // Assert
        Assert.Null(resultado);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve retornar inválido quando o autor não passar em uma validação")]
    public async Task DeveRetornarInvalido_QuandoAutorNaoPassarEmUmaValidacao()
    {
        // Arrange
        var autorEnviado = AutorMock.GerarObjetoInvalido();
        var autorValido = ValidationResultMock.GerarObjetoInvalido();

        _validator.Setup(validator => validator.ValidateAsync(It.IsAny<Autor>(), CancellationToken.None)).ReturnsAsync(autorValido);

        var servicoDominio = GerarCenario();

        // Act
        var resultado = await servicoDominio.CadastrarAsync(autorEnviado!, CancellationToken.None);

        // Assert
        Assert.Null(resultado);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve retornar erro qando ocorrer algum erro de infra")]
    public async Task DeveRetornarErro_QuandoHouverErroInfra()
    {
        // Arrange
        var autorParaCadastrar = AutorMock.GerarObjetoValido();
        var autorNaoCadastradoEncontrado = AutorMock.GerarObjetoNulo();
        var autorValidado = ValidationResultMock.GerarObjetoValido();

        _validator.Setup(validator => 
            validator.ValidateAsync(It.IsAny<Autor>(), CancellationToken.None))
        .ReturnsAsync(autorValidado);

        _repositorioAutor.Setup(repositorio => 
            repositorio.CadastrarAsync(It.IsAny<Autor>())
        ).ReturnsAsync(autorNaoCadastradoEncontrado!);

        var servicoDominio = GerarCenario();

        // Act
        var resultado = await servicoDominio.CadastrarAsync(autorParaCadastrar!, CancellationToken.None);

        // Assert
        Assert.Null(resultado);
        Assert.Equal(EResultadoAcaoServico.Erro, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve retornar o autor cadastrado quando cadastro for realizado com sucesso")]
    public async Task DeveRetornarAutorCadastrado_QuandoCadastroRealizadoComSucesso()
    {
        // Arrange
        var autorEnviado = AutorMock.GerarObjetoValido();
        var autorRetornado = autorEnviado;
        var autorValido = ValidationResultMock.GerarObjetoValido();

        _validator.Setup(validator => validator.ValidateAsync(It.IsAny<Autor>(), CancellationToken.None)).ReturnsAsync(autorValido);

        _repositorioAutor.Setup(repositorio =>
            repositorio.CadastrarAsync(It.IsAny<Autor>())
        ).ReturnsAsync(autorRetornado!);

        var servicoDominio = GerarCenario();

        // Act
        var resultado = await servicoDominio.CadastrarAsync(autorEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(EResultadoAcaoServico.Suceso, servicoDominio.ResultadoAcao);
        Assert.Empty(servicoDominio.Notifications);
    }
}