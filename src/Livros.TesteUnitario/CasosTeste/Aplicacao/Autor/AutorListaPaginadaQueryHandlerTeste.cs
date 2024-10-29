using AutoMapper;
using Livros.Aplicacao.CasosUso.Autor.Listar;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.DTOs;
using Livros.Dominio.Servicos.Autor.Listar;
using Livros.TesteUnitario.Mocks.Aplicacao.Autor;
using Livros.TesteUnitario.Mocks.Dominio.DTOs;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class AutorListaPaginadaQueryHandlerTeste
{
    private readonly Mock<IServicoListagemAutor> _servicoListagemAutor;
    private readonly Mock<IMapper> _mapper;

    public AutorListaPaginadaQueryHandlerTeste()
    {
        _servicoListagemAutor = new();
        _mapper = new();
    }

    private AutorListaPaginadaQueryHandler GerarCenario()
    {
        return new AutorListaPaginadaQueryHandler(
            _servicoListagemAutor.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Listar sem filtros selecionados")]
    public async Task ListarSemFiltrosSelecionados()
    {
        var query = AutorListaPaginadaQueryMock.GerarObjetoNulo();
        var filtros = AutorListaFiltroMock.GerarObjetoNulo();
        var listaPaginada = AutorListaPaginadaResultMock.GerarObjeto();
        var queryResult = AutorListaPaginadaQueryResultMock.GerarObjeto();

        _mapper.Setup(mapper =>
            mapper.Map<AutorListaFiltro>(It.IsAny<AutorListaPaginadaQuery>()))
        .Returns(filtros!);

        _servicoListagemAutor.Setup(autorListaDomainService =>
            autorListaDomainService.ListarAsync(It.IsAny<AutorListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(listaPaginada);

        _mapper.Setup(mapper =>
            mapper.Map<AutorListaPaginadaQueryResult>(It.IsAny<ListaPaginadaResult<Livros.Dominio.Entidades.Autor>>()))
        .Returns(queryResult!);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(queryResult);
        Assert.NotEmpty(queryResult.Itens!);
    }
}