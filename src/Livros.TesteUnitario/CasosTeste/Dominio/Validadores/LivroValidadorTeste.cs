using Bogus;
using FluentValidation.TestHelper;
using Livros.Dominio.Entidades;
using Livros.Dominio.Recursos;
using Livros.Dominio.Validadores;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;

namespace Livros.TesteUnitario.CasosTeste.Dominio.Validadores;

public class LivroValidadorTeste
{
    private LivroValidador GerarCenario()
    {
        return new LivroValidador();
    }

    [Fact(DisplayName = $"Deve retornar {Mensagens.LivroTituloECampoObrigatorio} quando o título não for enviado")]
    public void DeveRetornarNaoInformado_QuandoTituloForNulo()
    {
        // Arrange
        string? titulo = null;
        var livroEnviado = LivroMock.GerarObjetoValido();
        livroEnviado.AlterarTitulo(titulo!);

        // Act
        var resultado = GerarCenario().TestValidate(livroEnviado!);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.ErrorMessage == Mensagens.LivroTituloECampoObrigatorio);
    }

    [Fact(DisplayName = $"Deve retornar mensagem quando o título for maior que o tamanho máximo permitido")]
    public void DeveRetornarMensagemMaximaCaracteres_QuandoTituloForMaiorQuePermitido()
    {
        // Arrange
        string? titulo = new Faker().Random.String(Livro.TITULO_MAXIMO_CARACTERES + 1);
        var livroEnviado = LivroMock.GerarObjetoValido();
        livroEnviado.AlterarTitulo(titulo!);

        // Act
        var resultado = GerarCenario().TestValidate(livroEnviado!);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.ErrorMessage == Mensagens.LivroTituloPodeTerAteXCaracteres);
    }

    [Fact(DisplayName = $"Deve retornar {Mensagens.LivroEditoraECampoObrigatorio} quando o editora não for enviado")]
    public void DeveRetornarNaoInformado_QuandoEditoraForNulo()
    {
        // Arrange
        string? editora = null;
        var livroEnviado = LivroMock.GerarObjetoValido();
        livroEnviado.AlterarEditora(editora!);

        // Act
        var resultado = GerarCenario().TestValidate(livroEnviado!);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.ErrorMessage == Mensagens.LivroEditoraECampoObrigatorio);
    }

    [Fact(DisplayName = $"Deve retornar mensagem quando o título for maior que o tamanho máximo permitido")]
    public void DeveRetornarMensagem_QuandoEditoraForMaiorQuePermitido()
    {
        // Arrange
        string? editora = new Faker().Random.String(Livro.EDITORA_MAXIMO_CARACTERES + 1);
        var livroEnviado = LivroMock.GerarObjetoValido();
        livroEnviado.AlterarEditora(editora!);

        // Act
        var resultado = GerarCenario().TestValidate(livroEnviado!);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.ErrorMessage == Mensagens.LivroEditoraPodeTerAteXCaracteres);
    }

    [Fact(DisplayName = $"Deve retornar mensagem quando a edição for inválida")]
    public void DeveRetornarMensagem_QuandoEdicaoForInvalida()
    {
        // Arrange
        int edicao = 0;
        var livroEnviado = LivroMock.GerarObjetoValido();
        livroEnviado.AlterarEdicao(edicao);

        // Act
        var resultado = GerarCenario().TestValidate(livroEnviado!);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.ErrorMessage == Mensagens.LivroEdicaoInvalido);
    }

    [Fact(DisplayName = $"Deve retornar mensagem quando o ano de publicação for inválido")]
    public void DeveRetornarMensagem_QuandoAnoPublicacaoForInvalido()
    {
        // Arrange
        int anoPublicacao = 0;
        var livroEnviado = LivroMock.GerarObjetoValido();
        livroEnviado.AlterarAnoPublicacao(anoPublicacao);

        // Act
        var resultado = GerarCenario().TestValidate(livroEnviado!);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.ErrorMessage == Mensagens.LivroAnoPublicacaoInvalido);
    }

    [Fact(DisplayName = $"Deve retornar mensagem quando nenhum autor for enviado")]
    public void DeveRetornarMensagem_QuandoNenhmAutorForEnviado()
    {
        // Arrange
        var livroEnviado = LivroMock.GerarObjetoValido();
        livroEnviado.AlterarAutores(new());

        // Act
        var resultado = GerarCenario().TestValidate(livroEnviado!);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.ErrorMessage == Mensagens.LivroAutorECampoObrigatorio);
    }

    [Fact(DisplayName = $"Deve retornar mensagem quando nenhum assunto for enviado")]
    public void DeveRetornarMensagem_QuandoNenhmAssuntoForEnviado()
    {
        // Arrange
        var livroEnviado = LivroMock.GerarObjetoValido();
        livroEnviado.AlterarAssuntos(new());

        // Act
        var resultado = GerarCenario().TestValidate(livroEnviado!);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.ErrorMessage == Mensagens.LivroAssuntoECampoObrigatorio);
    }
}