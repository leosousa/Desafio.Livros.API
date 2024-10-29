using Bogus;
using FluentValidation.TestHelper;
using Livros.Dominio.Entidades;
using Livros.Dominio.Recursos;
using Livros.Dominio.Validadores;

namespace Livros.TesteUnitario.CasosTeste.Dominio.Validadores;

public class AutorValidadorTeste
{
    private AutorValidador GerarCenario()
    {
        return new AutorValidador();
    }

    [Fact(DisplayName = $"Deve retornar {Mensagens.NomeECampoObrigatorio} quando o nome não for enviado")]
    public async Task DeveRetornarNaoInformado_QuandoAutorForNulo()
    {
        // Arrange
        var autorEnviado = new Autor(nome: null);

        // Act
        var resultado = GerarCenario().TestValidate(autorEnviado!);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.ErrorMessage == Mensagens.NomeECampoObrigatorio);
    }

    [Fact(DisplayName = $"Deve retornar mensagem quando o nome for maior que o tamanho máximo permitido")]
    public async Task DeveRetornarMensagemMaximaCaracteres_QuandoAutorForMaiorQuePermitido()
    {
        // Arrange
        var autorEnviado = new Autor(nome: new Faker().Random.String(Autor.AUTOR_NOME_MAXIMO_CARACTERES + 1));

        // Act
        var resultado = GerarCenario().TestValidate(autorEnviado!);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.ErrorMessage == Mensagens.NomePodeTerAteXCaracteres);
    }
}