using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Entidades;
using Livros.Dominio.Servicos.Autor.Listar;
using Livros.TesteUnitario.Mocks.Dominio.DTOs;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;
using System.Linq.Expressions;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoListagemAutorTeste
{
    private Mock<IRepositorioAutor> _repositorioAutor;

    public ServicoListagemAutorTeste()
    {
        _repositorioAutor = new();
    }

    private ServicoListagemAutor GerarCenario()
    {
        return new ServicoListagemAutor(_repositorioAutor.Object);
    }

    [Fact(DisplayName = "Listar sem filtros selecionados")]
    public async Task ListarSemFiltrosSelecionados()
    {
        var filtros = AutorListaFiltroMock.GerarObjetoNulo();
        var listaSemItens = new List<Autor>();
        var numeroItens = listaSemItens.Count();

        _repositorioAutor.Setup(repository =>
            repository.CountAsync(It.IsAny<Expression<Func<Autor, bool>>>()))
        .ReturnsAsync(numeroItens);

        _repositorioAutor.Setup(repository =>
            repository.ListarAsync(It.IsAny<Expression<Func<Autor, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
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
        var filtros = AutorListaFiltroMock.GerarObjeto();
        var listaComItens = AutorMock.GerarObjetoLista(10);
        var numeroItens = listaComItens.Count();

        _repositorioAutor.Setup(repository =>
            repository.CountAsync(It.IsAny<Expression<Func<Autor, bool>>>()))
        .ReturnsAsync(numeroItens);

        _repositorioAutor.Setup(repository =>
            repository.ListarAsync(It.IsAny<Expression<Func<Autor, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(listaComItens);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.ListarAsync(filtros!);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Itens!);
        Assert.Empty(servicoDominio.Notifications);
    }
}