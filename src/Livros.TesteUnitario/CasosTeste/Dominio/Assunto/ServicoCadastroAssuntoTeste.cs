using FluentValidation;
using Livros.Dominio.Contratos;
using Livros.Dominio.Entidades;
using Livros.Dominio.Servicos.Assunto.Cadastrar;
using Livros.TesteUnitario.Mocks.Dominio;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoCadastroAssuntoTeste
{
    private readonly Mock<IValidator<Assunto>> _validator;
    private readonly Mock<IRepositorioAssunto> _repositorioAssunto;

    public ServicoCadastroAssuntoTeste()
    {
        _validator = new();
        _repositorioAssunto = new();
    }

    private ServicoCadastroAssunto GerarCenario()
    {
        return new ServicoCadastroAssunto(_validator.Object, _repositorioAssunto.Object);
    }

    [Fact(DisplayName = "Não deve tentar cadastrar quanto o assunto não for enviado")]
    public async Task NaoDeveCadastrar_QuandoAssuntoNulo()
    {
        // Arrange
        var assuntoEnviado = AssuntoMock.GerarObjetoNulo();

        // Act
        var resultado = await GerarCenario().CadastrarAsync(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.Equal(AssuntoErro.NaoInformado, resultado.Value);
    }

    [Fact(DisplayName = "Não deve tentar cadastrar quanto o assunto for inválido")]
    public async Task NaoDeveCadastrar_QuandoAssuntoForInvalido()
    {
        // Arrange
        var assuntoEnviado = AssuntoMock.GerarObjetoInvalido();
        var assuntoValido = ValidationResultMock.GerarObjetoInvalido();

        _validator.Setup(validator => validator.Validate(It.IsAny<Assunto>())).Returns(assuntoValido);

        // Act
        var resultado = await GerarCenario().CadastrarAsync(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.Equal(AssuntoErro.Invalido, resultado.Value);
    }

    [Fact(DisplayName = "Não deve cadastrar quando ocorrer um erro de infra")]
    public async Task NaoDeveCadastrar_QuandoHouverErroInfra()
    {
        // Arrange
        var assuntoEnviado = AssuntoMock.GerarObjetoValido();
        var assuntoRetornado = AssuntoMock.GerarObjetoNulo();
        var assuntoValido = ValidationResultMock.GerarObjetoValido();

        _validator.Setup(validator => validator.Validate(It.IsAny<Assunto>())).Returns(assuntoValido);

        _repositorioAssunto.Setup(repositorio => 
            repositorio.CadastrarAsync(It.IsAny<Assunto>())
        ).ReturnsAsync(assuntoRetornado!);

        // Act
        var resultado = await GerarCenario().CadastrarAsync(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.Equal(AssuntoErro.Erro, resultado.Value);
    }

    [Fact(DisplayName = "Deve cadastrar quando assunto for válido")]
    public async Task DeveCadastrar_QuandoAssuntoForValido()
    {
        // Arrange
        var assuntoEnviado = AssuntoMock.GerarObjetoValido();
        var assuntoRetornado = assuntoEnviado;
        var assuntoValido = ValidationResultMock.GerarObjetoValido();

        _validator.Setup(validator => validator.Validate(It.IsAny<Assunto>())).Returns(assuntoValido);

        _repositorioAssunto.Setup(repositorio =>
            repositorio.CadastrarAsync(It.IsAny<Assunto>())
        ).ReturnsAsync(assuntoRetornado!);

        // Act
        var resultado = await GerarCenario().CadastrarAsync(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado.Value);
    }
}