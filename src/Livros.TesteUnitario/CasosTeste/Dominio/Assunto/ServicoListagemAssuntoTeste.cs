using Livros.Dominio.Contratos;
using Livros.Dominio.Entidades;
using Livros.Dominio.Servicos.Assunto.Listar;
using Livros.TesteUnitario.Mocks.Dominio.DTOs;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;
using System.Linq.Expressions;

namespace Livros.TesteUnitario.CasosTeste.Dominio;

public class ServicoListagemAssuntoTeste
{
    private Mock<IRepositorioAssunto> _repositorioAssunto;

    public ServicoListagemAssuntoTeste()
    {
        _repositorioAssunto = new();
    }

    private ServicoListagemAssunto GerarCenario()
    {
        return new ServicoListagemAssunto(_repositorioAssunto.Object);
    }

    [Fact(DisplayName = "Listar sem filtros selecionados")]
    public async Task ListarSemFiltrosSelecionados()
    {
        var filtros = AssuntoListaFiltroMock.GerarObjetoNulo();
        var listaSemItens = new List<Assunto>();
        var numeroItens = listaSemItens.Count();

        _repositorioAssunto.Setup(repository =>
            repository.CountAsync(It.IsAny<Expression<Func<Assunto, bool>>>()))
        .ReturnsAsync(numeroItens);

        _repositorioAssunto.Setup(repository =>
            repository.ListarAsync(It.IsAny<Expression<Func<Assunto, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
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
        var filtros = AssuntoListaFiltroMock.GerarObjeto();
        var listaComItens = AssuntoMock.GerarObjetoLista(10);
        var numeroItens = listaComItens.Count();

        _repositorioAssunto.Setup(repository =>
            repository.CountAsync(It.IsAny<Expression<Func<Assunto, bool>>>()))
        .ReturnsAsync(numeroItens);

        _repositorioAssunto.Setup(repository =>
            repository.ListarAsync(It.IsAny<Expression<Func<Assunto, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(listaComItens);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.ListarAsync(filtros!);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Itens!);
        Assert.Empty(servicoDominio.Notifications);
    }
}