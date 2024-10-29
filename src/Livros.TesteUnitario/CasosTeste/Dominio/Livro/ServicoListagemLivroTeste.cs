using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Entidades;
using Livros.Dominio.Servicos.Livro.Listar;
using Livros.TesteUnitario.Mocks.Dominio.DTOs;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;
using System.Linq.Expressions;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoListagemLivroTeste
{
    private Mock<IRepositorioLivro> _repositorioLivro;

    public ServicoListagemLivroTeste()
    {
        _repositorioLivro = new();
    }

    private ServicoListagemLivro GerarCenario()
    {
        return new ServicoListagemLivro(_repositorioLivro.Object);
    }

    [Fact(DisplayName = "Listar sem filtros selecionados")]
    public async Task ListarSemFiltrosSelecionados()
    {
        var filtros = LivroListaFiltroMock.GerarObjetoNulo();
        var listaSemItens = new List<Livro>();
        var numeroItens = listaSemItens.Count();

        _repositorioLivro.Setup(repository =>
            repository.CountAsync(It.IsAny<Expression<Func<Livro, bool>>>()))
        .ReturnsAsync(numeroItens);

        _repositorioLivro.Setup(repository =>
            repository.ListarAsync(It.IsAny<Expression<Func<Livro, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(listaSemItens);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.ListarAsync(filtros!);

        Assert.NotNull(result);
        Assert.Empty(result.Itens!);
        Assert.Empty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Listar com filtros selecionados")]
    public async Task ListarComFiltrosSelecionados()
    {
        var filtros = LivroListaFiltroMock.GerarObjeto();
        var listaComItens = LivroMock.GerarObjetoLista(10);
        var numeroItens = listaComItens.Count();

        _repositorioLivro.Setup(repository =>
            repository.CountAsync(It.IsAny<Expression<Func<Livro, bool>>>()))
        .ReturnsAsync(numeroItens);

        _repositorioLivro.Setup(repository =>
            repository.ListarAsync(It.IsAny<Expression<Func<Livro, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(listaComItens);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.ListarAsync(filtros!);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Itens!);
        Assert.Empty(servicoDominio.Notifications);
    }
}