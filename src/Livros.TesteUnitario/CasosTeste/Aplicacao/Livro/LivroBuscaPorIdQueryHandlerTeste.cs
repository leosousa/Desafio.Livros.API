using AutoMapper;
using Livros.Aplicacao.CasosUso.Livro.BuscarPorId;
using Livros.Aplicacao.CasosUso.Livro.Cadastrar;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.Entidades;
using Livros.TesteUnitario.Mocks.Aplicacao.Livro;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class LivroBuscaPorIdQueryHandlerTeste
{
    private readonly Mock<IServicoBuscaLivroPorId> _servicoBuscaLivroPorId;
    private readonly Mock<IMapper> _mapper;

    public LivroBuscaPorIdQueryHandlerTeste()
    {
        _servicoBuscaLivroPorId = new();
        _mapper = new();
    }

    private LivroBuscaPorIdQueryHandler GerarCenario()
    {
        return new LivroBuscaPorIdQueryHandler(
            _servicoBuscaLivroPorId.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Não deve buscar se o identificador do livro não for informado")]
    public async Task NaoDeveBuscarLivroSeIdentificadorNaoForInformado()
    {
        var query = LivroBuscaPorIdQueryMock.GerarObjetoNulo();
        var livro = LivroMock.GerarObjeto();

        _mapper.Setup(mapper => mapper.Map<Livro>(It.IsAny<LivroCadastroCommand>())).Returns(livro);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Buscar livro que está cadastrado")]
    public async Task BuscarLivroQueEstaCadastrado()
    {
        var query = LivroBuscaPorIdQueryMock.GerarObjeto();
        var livro = LivroMock.GerarObjetoValido();
        var livroBuscaPorIdQueryResult = LivroBuscaPorIdQueryResultMock.GerarObjeto();

        _servicoBuscaLivroPorId.Setup(repository =>
            repository.BuscarPorIdAsync(query!.Id, CancellationToken.None))
        .ReturnsAsync(livro);

        _servicoBuscaLivroPorId.SetupGet(property => property.IsValid).Returns(true);

        _mapper.Setup(mapper => mapper.Map<LivroBuscaPorIdQueryResult>(It.IsAny<Livro>()))
            .Returns(livroBuscaPorIdQueryResult);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Notifications);
    }
}