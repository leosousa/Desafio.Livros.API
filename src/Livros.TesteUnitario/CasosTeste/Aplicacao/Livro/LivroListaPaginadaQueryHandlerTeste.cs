using AutoMapper;
using Livros.Aplicacao.CasosUso.Livro.Listar;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.DTOs;
using Livros.Dominio.Servicos.Livro.Listar;
using Livros.TesteUnitario.Mocks.Aplicacao.Livro;
using Livros.TesteUnitario.Mocks.Dominio.DTOs;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class LivroListaPaginadaQueryHandlerTeste
{
    private readonly Mock<IServicoListagemLivro> _servicoListagemLivro;
    private readonly Mock<IMapper> _mapper;

    public LivroListaPaginadaQueryHandlerTeste()
    {
        _servicoListagemLivro = new();
        _mapper = new();
    }

    private LivroListaPaginadaQueryHandler GerarCenario()
    {
        return new LivroListaPaginadaQueryHandler(
            _servicoListagemLivro.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Listar sem filtros selecionados")]
    public async Task ListarSemFiltrosSelecionados()
    {
        var query = LivroListaPaginadaQueryMock.GerarObjetoNulo();
        var filtros = LivroListaFiltroMock.GerarObjetoNulo();
        var listaPaginada = LivroListaPaginadaResultMock.GerarObjeto();
        var queryResult = LivroListaPaginadaQueryResultMock.GerarObjeto();

        _mapper.Setup(mapper =>
            mapper.Map<LivroListaFiltro>(It.IsAny<LivroListaPaginadaQuery>()))
        .Returns(filtros!);

        _servicoListagemLivro.Setup(livroListaDomainService =>
            livroListaDomainService.ListarAsync(It.IsAny<LivroListaFiltro>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(listaPaginada);

        _mapper.Setup(mapper =>
            mapper.Map<LivroListaPaginadaQueryResult>(It.IsAny<ListaPaginadaResult<Livros.Dominio.Entidades.Livro>>()))
        .Returns(queryResult!);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(queryResult);
        Assert.NotEmpty(queryResult.Itens!);
    }
}