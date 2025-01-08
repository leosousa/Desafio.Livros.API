using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Livros.TesteUnitario.CasosTeste.Dominio.Assunto;

public class ServicoCadastroAssuntoTeste
{
    [Fact(DisplayName = "N�o deve tentar cadastrar quanto o assunto n�o for enviado")]
    public void Teste_NaoDeveCadastrar_QuandoAssuntoNaoEnviado()
    {
        // Arrange
        var assuntoEnviado = AssuntoMock.GerarObjetoNulo();

        // Act
        var resultado = new { Erros = new List<string>() };

        // Assert
        Assert.Null(resultado);
        Assert.Contains(resultado!.Erros, erro => erro == "Assunto n�o enviado");
    }
}