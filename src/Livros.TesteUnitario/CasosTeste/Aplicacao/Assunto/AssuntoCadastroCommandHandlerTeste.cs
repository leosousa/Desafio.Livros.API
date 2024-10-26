using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;
using Livros.Dominio.Servicos.Assunto.Cadastrar;
using Livros.TesteUnitario.Mocks.Aplicacao.Assunto;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class AssuntoCadastroCommandHandlerTeste
{
    private AssuntoCadastroCommandHandler GerarCenario()
    {
        return new AssuntoCadastroCommandHandler();
    }

    [Fact(DisplayName = "Deve retornar \"não informado\" quanto o assunto não enviado")]
    public async Task DeveRetornarNaoInformado_QuandoAssuntoNaoForEnviado()
    {
        // Arrange
        var assuntoEnviado = AssuntoCadastroCommandMock.GerarObjetoNulo();

        // Act
        var resultado = await GerarCenario().Handle(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.Null(resultado);
    }
}