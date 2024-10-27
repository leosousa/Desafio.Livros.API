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

    [Fact(DisplayName = "Deve retornar \"não informado\" quanto o assunto não for enviado")]
    public async Task DeveRetornarNaoInformado_QuandoAssuntoForNulo()
    {
        // Arrange
        var assuntoEnviado = AssuntoMock.GerarObjetoNulo();

        // Act
        var resultado = await GerarCenario().CadastrarAsync(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.Equal(CadastroAssuntoRetorno.NaoInformado, resultado.Value);
    }

    [Fact(DisplayName = "Deve retornar inválido quando o assunto não passar em uma validação")]
    public async Task DeveRetornarInvalido_QuandoAssuntoNaoPassarEmUmaValidacao()
    {
        // Arrange
        var assuntoEnviado = AssuntoMock.GerarObjetoInvalido();
        var assuntoValido = ValidationResultMock.GerarObjetoInvalido();

        _validator.Setup(validator => validator.ValidateAsync(It.IsAny<Assunto>(), CancellationToken.None)).ReturnsAsync(assuntoValido);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.CadastrarAsync(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.Equal(CadastroAssuntoRetorno.Invalido, resultado.Value);
        Assert.NotEmpty(servico.Notifications);
    }

    [Fact(DisplayName = "Deve retornar erro qando ocorrer algum erro de infra")]
    public async Task DeveRetornarErro_QuandoHouverErroInfra()
    {
        // Arrange
        var assuntoEnviado = AssuntoMock.GerarObjetoValido();
        var assuntoRetornado = AssuntoMock.GerarObjetoNulo();
        var assuntoValido = ValidationResultMock.GerarObjetoValido();

        _validator.Setup(validator => validator.ValidateAsync(It.IsAny<Assunto>(), CancellationToken.None)).ReturnsAsync(assuntoValido);

        _repositorioAssunto.Setup(repositorio => 
            repositorio.CadastrarAsync(It.IsAny<Assunto>())
        ).ReturnsAsync(assuntoRetornado!);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.CadastrarAsync(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.Equal(CadastroAssuntoRetorno.Erro, resultado.Value);
        Assert.Empty(servico.Notifications);
    }

    [Fact(DisplayName = "Deve retornar o produto cadastrado quando cadastro for realizado com sucesso")]
    public async Task DeveRetornarProdutoCadastrado_QuandoCadastroRealizadoComSucesso()
    {
        // Arrange
        var assuntoEnviado = AssuntoMock.GerarObjetoValido();
        var assuntoRetornado = assuntoEnviado;
        var assuntoValido = ValidationResultMock.GerarObjetoValido();

        _validator.Setup(validator => validator.ValidateAsync(It.IsAny<Assunto>(), CancellationToken.None)).ReturnsAsync(assuntoValido);

        _repositorioAssunto.Setup(repositorio =>
            repositorio.CadastrarAsync(It.IsAny<Assunto>())
        ).ReturnsAsync(assuntoRetornado!);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.CadastrarAsync(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado.Value);
        Assert.Empty(servico.Notifications);
    }
}