using Bogus;
using FluentValidation.TestHelper;
using Livros.Dominio.Entidades;
using Livros.Dominio.Recursos;
using Livros.Dominio.Servicos.Assunto.Cadastrar;
using Livros.Dominio.Validadores;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using OneOf.Types;

namespace Livros.TesteUnitario.CasosTeste.Dominio.Validadores;

public class AssuntoValidadorTeste
{
    private AssuntoValidador GerarCenario()
    {
        return new AssuntoValidador();
    }

    [Fact(DisplayName = $"Deve retornar {Mensagens.AssuntoECampoObrigatorio} quando a descrição não for enviada")]
    public async Task DeveRetornarNaoInformado_QuandoAssuntoForNulo()
    {
        // Arrange
        var assuntoEnviado = new Assunto(descricao: null);

        // Act
        var resultado = GerarCenario().TestValidate(assuntoEnviado!);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.ErrorMessage == Mensagens.AssuntoECampoObrigatorio);
    }

    [Fact(DisplayName = $"Deve retornar mensagem quando a descrição for maior que o tamanho máximo permitido")]
    public async Task DeveRetornarMensagemMaximaCaracteres_QuandoAssuntoForMaiorQuePermitido()
    {
        // Arrange
        var assuntoEnviado = new Assunto(descricao: new Faker().Random.String(Assunto.ASSUNTO_DESCRICAO_MAXIMO_CARACTERES + 1));

        // Act
        var resultado = GerarCenario().TestValidate(assuntoEnviado!);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.ErrorMessage == Mensagens.AssuntoPodeTerAteXCaracteres);
    }
}