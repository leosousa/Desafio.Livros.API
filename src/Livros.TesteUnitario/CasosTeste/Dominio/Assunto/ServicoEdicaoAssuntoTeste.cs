using FluentValidation;
using Livros.Dominio.Contratos;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Servicos.Assunto.Editar;
using Livros.TesteUnitario.Mocks.Dominio;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoEdicaoAssuntoTeste
{
    private Mock<IRepositorioAssunto> _repositorioAssunto;
    private readonly Mock<IValidator<Assunto>> _validator;

    public ServicoEdicaoAssuntoTeste()
    {
        _repositorioAssunto = new();
        _validator = new();
    }

    private ServicoEdicaoAssunto GerarCenario()
    {
        return new ServicoEdicaoAssunto(_repositorioAssunto.Object, _validator.Object);
    }

    [Fact(DisplayName = "Não deve editar o assunto se o assunto não for enviado")]
    public async Task NaoDeveEditarAssuntoSeOMesmoNaoForEnviado()
    {
        var assuntoParaAlterar = AssuntoMock.GerarObjetoNulo();

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(assuntoParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o assunto se o assunto não for encontrado")]
    public async Task NaoDeveEditarAssuntoSeOMesmoNaoForEncontrado()
    {
        var assuntoParaAlterar = AssuntoMock.GerarObjetoValido();
        var assuntoNaoEncontrado = AssuntoMock.GerarObjetoNulo();

        _repositorioAssunto.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(assuntoNaoEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(assuntoParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.Equal(EResultadoAcaoServico.NaoEncontrado, servicoDominio.ResultadoAcao);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o assunto se dados inválidos")]
    public async Task NaoDeveEditarAssuntoSeDadosInvalidos()
    {
        var assuntoParaAlterar = AssuntoMock.GerarObjetoInvalido();
        var assuntoEncontrado = AssuntoMock.GerarObjetoValido();

        _repositorioAssunto.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(assuntoEncontrado);

        _validator.Setup(validator => 
            validator.ValidateAsync(It.IsAny<Assunto>(), CancellationToken.None))
        .ReturnsAsync(ValidationResultMock.GerarObjetoInvalido());

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(assuntoParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não Deve editar o assunto em caso de erro de infra")]
    public async Task NaoDeveEditarAssuntoSeHouverErroInfra()
    {
        var assuntoParaAlterar = AssuntoMock.GerarObjetoValido();
        var assuntoEncontrado = AssuntoMock.GerarObjetoValido();
        Assunto? assuntoAlterado = null;

        _repositorioAssunto.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(assuntoEncontrado);

        _repositorioAssunto.Setup(repositorio =>
           repositorio.EditarAsync(It.IsAny<Assunto>()))
       .ReturnsAsync(assuntoAlterado!);

        _validator.Setup(validator =>
            validator.ValidateAsync(It.IsAny<Assunto>(), CancellationToken.None))
        .ReturnsAsync(ValidationResultMock.GerarObjetoValido());

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(assuntoParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.Equal(EResultadoAcaoServico.Erro, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve editar o assunto se dados informados estão corretos")]
    public async Task DeveEditarAssuntoSeDadosEstaoCorretos()
    {
        var assuntoParaAlterar = AssuntoMock.GerarObjetoValido();
        var assuntoEncontrado = AssuntoMock.GerarObjetoValido();
        var assuntoAlterado = assuntoParaAlterar;

        _repositorioAssunto.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(assuntoEncontrado);

        _repositorioAssunto.Setup(repositorio =>
           repositorio.EditarAsync(It.IsAny<Assunto>()))
       .ReturnsAsync(assuntoAlterado);

        _validator.Setup(validator =>
            validator.ValidateAsync(It.IsAny<Assunto>(), CancellationToken.None))
        .ReturnsAsync(ValidationResultMock.GerarObjetoValido());

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(assuntoParaAlterar!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(EResultadoAcaoServico.Suceso, servicoDominio.ResultadoAcao);
        Assert.Empty(servicoDominio.Notifications);
    }
}