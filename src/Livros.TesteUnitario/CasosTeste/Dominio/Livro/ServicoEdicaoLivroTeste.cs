using FluentValidation;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Servicos.Livro.Editar;
using Livros.TesteUnitario.Mocks.Dominio;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoEdicaoLivroTeste
{
    private Mock<IRepositorioLivro> _repositorioLivro;
    private readonly Mock<IValidator<Livro>> _validator;

    public ServicoEdicaoLivroTeste()
    {
        _repositorioLivro = new();
        _validator = new();
    }

    private ServicoEdicaoLivro GerarCenario()
    {
        return new ServicoEdicaoLivro(_repositorioLivro.Object, _validator.Object);
    }

    [Fact(DisplayName = "Não deve editar o livro se o livro não for enviado")]
    public async Task NaoDeveEditarLivroSeOMesmoNaoForEnviado()
    {
        var livroParaAlterar = LivroMock.GerarObjetoNulo();

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(livroParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o livro se o livro não for encontrado")]
    public async Task NaoDeveEditarLivroSeOMesmoNaoForEncontrado()
    {
        var livroParaAlterar = LivroMock.GerarObjetoValido();
        var livroNaoEncontrado = LivroMock.GerarObjetoNulo();

        _repositorioLivro.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(livroNaoEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(livroParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.Equal(EResultadoAcaoServico.NaoEncontrado, servicoDominio.ResultadoAcao);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o livro se dados inválidos")]
    public async Task NaoDeveEditarLivroSeDadosInvalidos()
    {
        var livroParaAlterar = LivroMock.GerarObjeto();
        var livroEncontrado = LivroMock.GerarObjetoValido();

        _repositorioLivro.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(livroEncontrado);

        _validator.Setup(validator => 
            validator.ValidateAsync(It.IsAny<Livro>(), CancellationToken.None))
        .ReturnsAsync(ValidationResultMock.GerarObjetoInvalido());

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(livroParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não Deve editar o livro em caso de erro de infra")]
    public async Task NaoDeveEditarLivroSeHouverErroInfra()
    {
        var livroParaAlterar = LivroMock.GerarObjetoValido();
        var livroEncontrado = LivroMock.GerarObjetoValido();
        Livro? livroAlterado = null;

        _repositorioLivro.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(livroEncontrado);

        _repositorioLivro.Setup(repositorio =>
           repositorio.EditarAsync(It.IsAny<Livro>()))
       .ReturnsAsync(livroAlterado!);

        _validator.Setup(validator =>
            validator.ValidateAsync(It.IsAny<Livro>(), CancellationToken.None))
        .ReturnsAsync(ValidationResultMock.GerarObjetoValido());

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(livroParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.Equal(EResultadoAcaoServico.Erro, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve editar o livro se dados informados estão corretos")]
    public async Task DeveEditarLivroSeDadosEstaoCorretos()
    {
        var livroParaAlterar = LivroMock.GerarObjetoValido();
        var livroEncontrado = LivroMock.GerarObjetoValido();
        var livroAlterado = livroParaAlterar;

        _repositorioLivro.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(livroEncontrado);

        _repositorioLivro.Setup(repositorio =>
           repositorio.EditarAsync(It.IsAny<Livro>()))
       .ReturnsAsync(livroAlterado);

        _validator.Setup(validator =>
            validator.ValidateAsync(It.IsAny<Livro>(), CancellationToken.None))
        .ReturnsAsync(ValidationResultMock.GerarObjetoValido());

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(livroParaAlterar!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(EResultadoAcaoServico.Suceso, servicoDominio.ResultadoAcao);
        Assert.Empty(servicoDominio.Notifications);
    }
}