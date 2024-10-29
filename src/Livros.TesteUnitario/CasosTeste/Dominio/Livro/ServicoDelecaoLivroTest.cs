using Bogus;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Servicos.Livro.Deletar;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoDelecaoLivroTest
{
    private Mock<IRepositorioLivro> _repositorioLivro;

    public ServicoDelecaoLivroTest()
    {
        _repositorioLivro = new();
    }

    private ServicoDelecaoLivro GerarCenario()
    {
        return new ServicoDelecaoLivro(_repositorioLivro.Object);
    }

    [Fact(DisplayName = "Não deve deletar o livro se o id for maior ou igual a 0")]
    public async Task NaoDeveDeletarLivroSeOMesmoNaoForEnviado()
    {
        var id = new Faker().Random.Int(min: int.MinValue, max: 0);

        var servicoDominio = GerarCenario();

        var livroNaoRemovido = await servicoDominio.RemoverAsync(id!, CancellationToken.None);

        Assert.False(livroNaoRemovido);
        Assert.Equal(EResultadoAcaoServico.ParametrosInvalidos, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve deletar o livro se o mesmo não for encontrado")]
    public async Task NaoDeveDeletarLivroSeOMesmoNaoForEncontrado()
    {
        var id = new Faker().Random.Int(min: 1, max: 100);

        var livroNaoEncontrado = LivroMock.GerarObjetoNulo();

        _repositorioLivro.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(livroNaoEncontrado);

        var servicoDominio = GerarCenario();

        var livroNaoRemovido = await servicoDominio.RemoverAsync(id!, CancellationToken.None);

        Assert.False(livroNaoRemovido);
        Assert.Equal(EResultadoAcaoServico.NaoEncontrado, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não Deve deletar o livro em caso de erro de infra")]
    public async Task NaoDeveDeletarLivroSeHouverErroInfra()
    {
        var id = new Faker().Random.Int(min: 1, max: 100);
        var livroEncontrado = LivroMock.GerarObjetoValido();
        var livroDeletado = false;

        _repositorioLivro.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(livroEncontrado);

        _repositorioLivro.Setup(repositorio =>
           repositorio.RemoverAsync(It.IsAny<Livro>()))
       .ReturnsAsync(livroDeletado!);

        var servicoDominio = GerarCenario();

        var livroNaoRemovido = await servicoDominio.RemoverAsync(id!, CancellationToken.None);

        Assert.False(livroNaoRemovido);
        Assert.Equal(EResultadoAcaoServico.Erro, servicoDominio.ResultadoAcao);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve deletar o livro se o id informado está corretos")]
    public async Task DeveDeletarLivroSeIdInformadoEstaValido()
    {
        var id = new Faker().Random.Int(min: 1, max: 100);
        var livroEncontrado = LivroMock.GerarObjetoValido();
        var livroDeletado = true;

        _repositorioLivro.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(livroEncontrado);

        _repositorioLivro.Setup(repositorio =>
           repositorio.RemoverAsync(It.IsAny<Livro>()))
       .ReturnsAsync(livroDeletado!);

        var servicoDominio = GerarCenario();

        var livroNaoRemovido = await servicoDominio.RemoverAsync(id!, CancellationToken.None);

        Assert.True(livroNaoRemovido);
        Assert.Equal(EResultadoAcaoServico.Suceso, servicoDominio.ResultadoAcao);
        Assert.Empty(servicoDominio.Notifications);
    }
}