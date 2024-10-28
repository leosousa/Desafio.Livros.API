using AutoMapper;
using Livros.Aplicacao.CasosUso.Assunto.BuscarPorId;
using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Entidades;
using Livros.TesteUnitario.Mocks.Aplicacao.Assunto;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class AssuntoBuscaPorIdQueryHandlerTeste
{
    private readonly Mock<IServicoBuscaAssuntoPorId> _servicoBuscaAssuntoPorId;
    private readonly Mock<IMapper> _mapper;

    public AssuntoBuscaPorIdQueryHandlerTeste()
    {
        _servicoBuscaAssuntoPorId = new();
        _mapper = new();
    }

    private AssuntoBuscaPorIdQueryHandler GerarCenario()
    {
        return new AssuntoBuscaPorIdQueryHandler(
            _servicoBuscaAssuntoPorId.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Não deve buscar se o identificador do assunto não for informado")]
    public async Task NaoDeveBuscarAssuntoSeIdentificadorNaoForInformado()
    {
        var query = AssuntoBuscaPorIdQueryMock.GerarObjetoNulo();
        var assunto = AssuntoMock.GerarObjetoInvalido();

        _mapper.Setup(mapper => mapper.Map<Assunto>(It.IsAny<AssuntoCadastroCommand>())).Returns(assunto);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Buscar assunto que está cadastrado")]
    public async Task BuscarAssuntoQueEstaCadastrado()
    {
        var query = AssuntoBuscaPorIdQueryMock.GerarObjeto();
        var assunto = AssuntoMock.GerarObjetoValido();
        var assuntoBuscaPorIdQueryResult = AssuntoBuscaPorIdQueryResultMock.GerarObjeto();

        _servicoBuscaAssuntoPorId.Setup(repository =>
            repository.BuscarPorIdAsync(query!.Id, CancellationToken.None))
        .ReturnsAsync(assunto);

        _servicoBuscaAssuntoPorId.SetupGet(property => property.IsValid).Returns(true);

        _mapper.Setup(mapper => mapper.Map<AssuntoBuscaPorIdQueryResult>(It.IsAny<Assunto>()))
            .Returns(assuntoBuscaPorIdQueryResult);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Notifications);
    }
}