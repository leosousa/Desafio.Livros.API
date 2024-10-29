using AutoMapper;
using Livros.Aplicacao.CasosUso.Autor.BuscarPorId;
using Livros.Aplicacao.CasosUso.Autor.Cadastrar;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Entidades;
using Livros.TesteUnitario.Mocks.Aplicacao.Autor;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class AutorBuscaPorIdQueryHandlerTeste
{
    private readonly Mock<IServicoBuscaAutorPorId> _servicoBuscaAutorPorId;
    private readonly Mock<IMapper> _mapper;

    public AutorBuscaPorIdQueryHandlerTeste()
    {
        _servicoBuscaAutorPorId = new();
        _mapper = new();
    }

    private AutorBuscaPorIdQueryHandler GerarCenario()
    {
        return new AutorBuscaPorIdQueryHandler(
            _servicoBuscaAutorPorId.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Não deve buscar se o identificador do autor não for informado")]
    public async Task NaoDeveBuscarAutorSeIdentificadorNaoForInformado()
    {
        var query = AutorBuscaPorIdQueryMock.GerarObjetoNulo();
        var autor = AutorMock.GerarObjetoInvalido();

        _mapper.Setup(mapper => mapper.Map<Autor>(It.IsAny<AutorCadastroCommand>())).Returns(autor);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Buscar autor que está cadastrado")]
    public async Task BuscarAutorQueEstaCadastrado()
    {
        var query = AutorBuscaPorIdQueryMock.GerarObjeto();
        var autor = AutorMock.GerarObjetoValido();
        var autorBuscaPorIdQueryResult = AutorBuscaPorIdQueryResultMock.GerarObjeto();

        _servicoBuscaAutorPorId.Setup(repository =>
            repository.BuscarPorIdAsync(query!.Id, CancellationToken.None))
        .ReturnsAsync(autor);

        _servicoBuscaAutorPorId.SetupGet(property => property.IsValid).Returns(true);

        _mapper.Setup(mapper => mapper.Map<AutorBuscaPorIdQueryResult>(It.IsAny<Autor>()))
            .Returns(autorBuscaPorIdQueryResult);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Notifications);
    }
}