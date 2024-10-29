using FluentValidation;
using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Servicos.Autor.Editar;
using Livros.TesteUnitario.Mocks.Dominio;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoEdicaoAutorTeste
{
    private Mock<IRepositorioAutor> _repositorioAutor;
    private readonly Mock<IValidator<Autor>> _validator;

    public ServicoEdicaoAutorTeste()
    {
        _repositorioAutor = new();
        _validator = new();
    }

    private ServicoEdicaoAutor GerarCenario()
    {
        return new ServicoEdicaoAutor(_repositorioAutor.Object, _validator.Object);
    }

    [Fact(DisplayName = "Não deve editar o autor se o autor não for enviado")]
    public async Task NaoDeveEditarAutorSeOMesmoNaoForEnviado()
    {
        var autorParaAlterar = AutorMock.GerarObjetoNulo();

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(autorParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o autor se o autor não for encontrado")]
    public async Task NaoDeveEditarAutorSeOMesmoNaoForEncontrado()
    {
        var autorParaAlterar = AutorMock.GerarObjetoValido();
        var autorNaoEncontrado = AutorMock.GerarObjetoNulo();

        _repositorioAutor.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(autorNaoEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(autorParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.Equal(EResultadoAcaoServico.NaoEncontrado, servicoDominio.ResultadoAcao);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o autor se dados inválidos")]
    public async Task NaoDeveEditarAutorSeDadosInvalidos()
    {
        var autorParaAlterar = AutorMock.GerarObjetoInvalido();
        var autorEncontrado = AutorMock.GerarObjetoValido();

        _repositorioAutor.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(autorEncontrado);

        _validator.Setup(validator => 
            validator.ValidateAsync(It.IsAny<Autor>(), CancellationToken.None))
        .ReturnsAsync(ValidationResultMock.GerarObjetoInvalido());

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(autorParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não Deve editar o autor em caso de erro de infra")]
    public async Task NaoDeveEditarAutorSeHouverErroInfra()
    {
        var autorParaAlterar = AutorMock.GerarObjetoValido();
        var autorEncontrado = AutorMock.GerarObjetoValido();
        Autor? autorAlterado = null;

        _repositorioAutor.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(autorEncontrado);

        _repositorioAutor.Setup(repositorio =>
           repositorio.EditarAsync(It.IsAny<Autor>()))
       .ReturnsAsync(autorAlterado!);

        _validator.Setup(validator =>
            validator.ValidateAsync(It.IsAny<Autor>(), CancellationToken.None))
        .ReturnsAsync(ValidationResultMock.GerarObjetoValido());

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(autorParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.Equal(EResultadoAcaoServico.Erro, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve editar o autor se dados informados estão corretos")]
    public async Task DeveEditarAutorSeDadosEstaoCorretos()
    {
        var autorParaAlterar = AutorMock.GerarObjetoValido();
        var autorEncontrado = AutorMock.GerarObjetoValido();
        var autorAlterado = autorParaAlterar;

        _repositorioAutor.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(autorEncontrado);

        _repositorioAutor.Setup(repositorio =>
           repositorio.EditarAsync(It.IsAny<Autor>()))
       .ReturnsAsync(autorAlterado);

        _validator.Setup(validator =>
            validator.ValidateAsync(It.IsAny<Autor>(), CancellationToken.None))
        .ReturnsAsync(ValidationResultMock.GerarObjetoValido());

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(autorParaAlterar!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(EResultadoAcaoServico.Suceso, servicoDominio.ResultadoAcao);
        Assert.Empty(servicoDominio.Notifications);
    }
}