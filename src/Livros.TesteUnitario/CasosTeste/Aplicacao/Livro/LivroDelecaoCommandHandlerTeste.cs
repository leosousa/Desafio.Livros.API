using Livros.Aplicacao.CasosUso.Livro.Deletar;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.Enumeracoes;
using Livros.TesteUnitario.Mocks;
using Livros.TesteUnitario.Mocks.Aplicacao.Livro.Deletar;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class LivroDelecaoCommandHandlerTeste
{
    private readonly Mock<IServicoDelecaoLivro> _servicoDelecaoLivro;

    public LivroDelecaoCommandHandlerTeste()
    {
        _servicoDelecaoLivro = new();
    }

    private LivroDelecaoCommandHandler GerarCenario()
    {
        return new LivroDelecaoCommandHandler(
            _servicoDelecaoLivro.Object
        );
    }

    [Fact(DisplayName = "Não deve deletar se o identificador do livro não for informado")]
    public async Task NaoDeveBuscarLivroSeIdentificadorNaoForInformado()
    {
        var query = LivroDelecaoCommandMock.GerarObjetoNulo();
        var livro = LivroMock.GerarObjeto();

        _servicoDelecaoLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.ParametrosInvalidos);
        _servicoDelecaoLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoDelecaoLivro.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve deletar o livro se o mesmo não for encontrado")]
    public async Task NaoDeveDeletarLivroSeOMesmoNaoForEncontrado()
    {
        var command = LivroDelecaoCommandMock.GerarObjeto();
        var livro = LivroMock.GerarObjeto();

        _servicoDelecaoLivro.Setup(service =>
            service.RemoverAsync(It.IsAny<int>(), CancellationToken.None))
        .ReturnsAsync(false);

        _servicoDelecaoLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.NaoEncontrado);
        _servicoDelecaoLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoDelecaoLivro.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve deletar o livro se houver um erro de infra")]
    public async Task NaoDeveDeletarLivroSeHouverErroInfra()
    {
        var command = LivroDelecaoCommandMock.GerarObjeto();
        var livro = LivroMock.GerarObjeto();

        _servicoDelecaoLivro.Setup(service =>
            service.RemoverAsync(It.IsAny<int>(), CancellationToken.None))
        .ReturnsAsync(false);

        _servicoDelecaoLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Erro);
        _servicoDelecaoLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoDelecaoLivro.SetupGet(property => property.IsValid).Returns(false);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Deve deletar o livro se os dados forem válidos")]
    public async Task DeveDeletarLivroSeDadosForemValidos()
    {
        var command = LivroDelecaoCommandMock.GerarObjeto();
        var livro = LivroMock.GerarObjetoValido();
        var livroDelecaoCommandResult = LivroDelecaoCommandResultMock.GerarObjeto();

        _servicoDelecaoLivro.Setup(service =>
            service.RemoverAsync(It.IsAny<int>(), CancellationToken.None))
        .ReturnsAsync(true);

        _servicoDelecaoLivro.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoDelecaoLivro.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoDelecaoLivro.SetupGet(property => property.IsValid).Returns(true);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Notifications);
    }
}