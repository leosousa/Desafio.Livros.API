using FluentValidation;
using Livros.Dominio.Contratos;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Servicos;
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
        return new ServicoCadastroAssunto(_repositorioAssunto.Object, _validator.Object);
    }

    [Fact(DisplayName = "Deve retornar \"não informado\" quanto o assunto não for enviado")]
    public async Task DeveRetornarNaoInformado_QuandoAssuntoForNulo()
    {
        // Arrange
        var assuntoEnviado = AssuntoMock.GerarObjetoNulo();

        var servicoDominio = GerarCenario();

        // Act
        var resultado = await servicoDominio.CadastrarAsync(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.Null(resultado);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve retornar inválido quando o assunto não passar em uma validação")]
    public async Task DeveRetornarInvalido_QuandoAssuntoNaoPassarEmUmaValidacao()
    {
        // Arrange
        var assuntoEnviado = AssuntoMock.GerarObjetoInvalido();
        var assuntoValido = ValidationResultMock.GerarObjetoInvalido();

        _validator.Setup(validator => validator.ValidateAsync(It.IsAny<Assunto>(), CancellationToken.None)).ReturnsAsync(assuntoValido);

        var servicoDominio = GerarCenario();

        // Act
        var resultado = await servicoDominio.CadastrarAsync(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.Null(resultado);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve retornar erro qando ocorrer algum erro de infra")]
    public async Task DeveRetornarErro_QuandoHouverErroInfra()
    {
        // Arrange
        var assuntoParaCadastrar = AssuntoMock.GerarObjetoValido();
        var assuntoNaoCadastradoEncontrado = AssuntoMock.GerarObjetoNulo();
        var assuntoValidado = ValidationResultMock.GerarObjetoValido();

        _validator.Setup(validator => 
            validator.ValidateAsync(It.IsAny<Assunto>(), CancellationToken.None))
        .ReturnsAsync(assuntoValidado);

        _repositorioAssunto.Setup(repositorio => 
            repositorio.CadastrarAsync(It.IsAny<Assunto>())
        ).ReturnsAsync(assuntoNaoCadastradoEncontrado!);

        var servicoDominio = GerarCenario();

        // Act
        var resultado = await servicoDominio.CadastrarAsync(assuntoParaCadastrar!, CancellationToken.None);

        // Assert
        Assert.Null(resultado);
        Assert.Equal(EResultadoAcaoServico.Erro, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve retornar o assunto cadastrado quando cadastro for realizado com sucesso")]
    public async Task DeveRetornarAssuntoCadastrado_QuandoCadastroRealizadoComSucesso()
    {
        // Arrange
        var assuntoEnviado = AssuntoMock.GerarObjetoValido();
        var assuntoRetornado = assuntoEnviado;
        var assuntoValido = ValidationResultMock.GerarObjetoValido();

        _validator.Setup(validator => validator.ValidateAsync(It.IsAny<Assunto>(), CancellationToken.None)).ReturnsAsync(assuntoValido);

        _repositorioAssunto.Setup(repositorio =>
            repositorio.CadastrarAsync(It.IsAny<Assunto>())
        ).ReturnsAsync(assuntoRetornado!);

        var servicoDominio = GerarCenario();

        // Act
        var resultado = await servicoDominio.CadastrarAsync(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(EResultadoAcaoServico.Suceso, servicoDominio.ResultadoAcao);
        Assert.Empty(servicoDominio.Notifications);
    }
}