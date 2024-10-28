using AutoMapper;
using Livros.Aplicacao.CasosUso.Assunto.Listar;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.DTOs;
using Livros.Dominio.Servicos.Assunto.Listar;
using Livros.TesteUnitario.Mocks.Aplicacao.Assunto;
using Livros.TesteUnitario.Mocks.Dominio.DTOs;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class AssuntoListaPaginadaQueryHandlerTeste
{
    private readonly Mock<IServicoListagemAssunto> _servicoListagemAssunto;
    private readonly Mock<IMapper> _mapper;

    public AssuntoListaPaginadaQueryHandlerTeste()
    {
        _servicoListagemAssunto = new();
        _mapper = new();
    }

    private AssuntoListaPaginadaQueryHandler GerarCenario()
    {
        return new AssuntoListaPaginadaQueryHandler(
            _servicoListagemAssunto.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Listar sem filtros selecionados")]
    public async Task ListarSemFiltrosSelecionados()
    {
        var query = AssuntoListaPaginadaQueryMock.GerarObjetoNulo();
        var filtros = AssuntoListaFiltroMock.GerarObjetoNulo();
        var listaPaginada = AssuntoListaPaginadaResultMock.GerarObjeto();
        var queryResult = AssuntoListaPaginadaQueryResultMock.GerarObjeto();

        _mapper.Setup(mapper =>
            mapper.Map<AssuntoListaFiltro>(It.IsAny<AssuntoListaPaginadaQuery>()))
        .Returns(filtros!);

        _servicoListagemAssunto.Setup(assuntoListaDomainService =>
            assuntoListaDomainService.ListarAsync(It.IsAny<AssuntoListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(listaPaginada);

        _mapper.Setup(mapper =>
            mapper.Map<AssuntoListaPaginadaQueryResult>(It.IsAny<ListaPaginadaResult<Livros.Dominio.Entidades.Assunto>>()))
        .Returns(queryResult!);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(queryResult);
        Assert.NotEmpty(queryResult.Itens!);
    }
}