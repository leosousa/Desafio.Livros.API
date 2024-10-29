using FluentValidation;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Servicos.Livro.Cadastrar;
using Livros.TesteUnitario.Mocks.Dominio;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoCadastroLivroTeste
{
    private readonly Mock<IValidator<Livro>> _validator;
    private readonly Mock<IRepositorioLivro> _repositorioLivro;

    public ServicoCadastroLivroTeste()
    {
        _validator = new();
        _repositorioLivro = new();
    }

    private ServicoCadastroLivro GerarCenario()
    {
        return new ServicoCadastroLivro(_repositorioLivro.Object, _validator.Object);
    }

    [Fact(DisplayName = "Deve retornar \"não informado\" quanto o livro não for enviado")]
    public async Task DeveRetornarNaoInformado_QuandoLivroForNulo()
    {
        // Arrange
        var livroEnviado = LivroMock.GerarObjetoNulo();

        var servicoDominio = GerarCenario();

        // Act
        var resultado = await servicoDominio.CadastrarAsync(livroEnviado!, CancellationToken.None);

        // Assert
        Assert.Null(resultado);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve retornar inválido quando o livro não passar em uma validação")]
    public async Task DeveRetornarInvalido_QuandoLivroNaoPassarEmUmaValidacao()
    {
        // Arrange
        var livroEnviado = LivroMock.GerarObjeto();
        var livroValido = ValidationResultMock.GerarObjetoInvalido();

        _validator.Setup(validator => validator.ValidateAsync(It.IsAny<Livro>(), CancellationToken.None)).ReturnsAsync(livroValido);

        var servicoDominio = GerarCenario();

        // Act
        var resultado = await servicoDominio.CadastrarAsync(livroEnviado!, CancellationToken.None);

        // Assert
        Assert.Null(resultado);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve retornar erro qando ocorrer algum erro de infra")]
    public async Task DeveRetornarErro_QuandoHouverErroInfra()
    {
        // Arrange
        var livroParaCadastrar = LivroMock.GerarObjetoValido();
        var livroNaoCadastradoEncontrado = LivroMock.GerarObjetoNulo();
        var livroValidado = ValidationResultMock.GerarObjetoValido();

        _validator.Setup(validator => 
            validator.ValidateAsync(It.IsAny<Livro>(), CancellationToken.None))
        .ReturnsAsync(livroValidado);

        _repositorioLivro.Setup(repositorio => 
            repositorio.CadastrarAsync(It.IsAny<Livro>())
        ).ReturnsAsync(livroNaoCadastradoEncontrado!);

        var servicoDominio = GerarCenario();

        // Act
        var resultado = await servicoDominio.CadastrarAsync(livroParaCadastrar!, CancellationToken.None);

        // Assert
        Assert.Null(resultado);
        Assert.Equal(EResultadoAcaoServico.Erro, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve retornar o livro cadastrado quando cadastro for realizado com sucesso")]
    public async Task DeveRetornarLivroCadastrado_QuandoCadastroRealizadoComSucesso()
    {
        // Arrange
        var livroEnviado = LivroMock.GerarObjetoValido();
        var livroRetornado = livroEnviado;
        var livroValido = ValidationResultMock.GerarObjetoValido();

        _validator.Setup(validator => validator.ValidateAsync(It.IsAny<Livro>(), CancellationToken.None)).ReturnsAsync(livroValido);

        _repositorioLivro.Setup(repositorio =>
            repositorio.CadastrarAsync(It.IsAny<Livro>())
        ).ReturnsAsync(livroRetornado!);

        var servicoDominio = GerarCenario();

        // Act
        var resultado = await servicoDominio.CadastrarAsync(livroEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(EResultadoAcaoServico.Suceso, servicoDominio.ResultadoAcao);
        Assert.Empty(servicoDominio.Notifications);
    }
}