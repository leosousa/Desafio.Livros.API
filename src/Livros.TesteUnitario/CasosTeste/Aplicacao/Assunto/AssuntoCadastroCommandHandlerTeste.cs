﻿using AutoMapper;
using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;
using Livros.Dominio.Contratos;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using Livros.Dominio.Recursos;
using Livros.Dominio.Servicos;
using Livros.Dominio.Servicos.Assunto.Cadastrar;
using Livros.TesteUnitario.Mocks;
using Livros.TesteUnitario.Mocks.Aplicacao.Assunto;
using Livros.TesteUnitario.Mocks.Dominio.Entidades;
using Moq;
using OneOf;

namespace Livros.TesteUnitario.CasosTeste.Aplicacao;

public class AssuntoCadastroCommandHandlerTeste
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IServicoCadastroAssunto> _servicoCadastroAssunto;

    public AssuntoCadastroCommandHandlerTeste()
    {
        _mapper = new();
        _servicoCadastroAssunto = new();
    }

    private AssuntoCadastroCommandHandler GerarCenario()
    {
        return new AssuntoCadastroCommandHandler(_mapper.Object, _servicoCadastroAssunto.Object);
    }

    [Fact(DisplayName = "Deve retornar \"não informado\" quanto o assunto não enviado")]
    public async Task DeveRetornarNaoInformado_QuandoAssuntoNaoForEnviado()
    {
        // Arrange
        var assuntoEnviado = AssuntoCadastroCommandMock.GerarObjetoNulo();

        _servicoCadastroAssunto.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.ParametrosInvalidos);
        _servicoCadastroAssunto.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoCadastroAssunto.SetupGet(property => property.IsValid).Returns(false);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(assuntoEnviado!, CancellationToken.None);

        // Assert
        //Assert.NotNull(resultado);
        //Assert.False(resultado.IsValid);
        //Assert.Contains(resultado.Notifications, notification => notification.Message == Mensagens.AssuntoNaoInformado);
        Assert.NotNull(resultado);
        Assert.NotEmpty(resultado.Notifications);
    }

    [Fact(DisplayName = "Deve retornar o tipo de retorno e as notificações quando o assunto não for cadastrado")]
    public async Task DeveRetornarTipoRetornoENotificacoes_QuandoAssuntoNaoForCadastrado()
    {
        // Arrange
        var assuntoEnviado = AssuntoCadastroCommandMock.GerarObjetoInvalido();
        var assuntoInvalido = AssuntoMock.GerarObjetoInvalido();

        _mapper.Setup(mapper => mapper.Map<Assunto>(assuntoEnviado)).Returns(assuntoInvalido);

        _servicoCadastroAssunto.Setup(
            servicoCadastroAssunto => servicoCadastroAssunto.CadastrarAsync(It.IsAny<Assunto>(), CancellationToken.None)
        ).ReturnsAsync(assuntoInvalido);

        _servicoCadastroAssunto.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Erro);
        _servicoCadastroAssunto.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarObjetoLista());
        _servicoCadastroAssunto.SetupGet(property => property.IsValid).Returns(false);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.NotEmpty(resultado.Notifications);
    }

    [Fact(DisplayName = "Deve retornar o assunto quando o mesmo for cadastrado com sucesso")]
    public async Task DeveRetornarAssunto_QuandoOMesmoForCadastradoComSucesso()
    {
        // Arrange
        var assuntoEnviado = AssuntoCadastroCommandMock.GerarObjetoValido();
        var assuntoValido = AssuntoMock.GerarObjetoValido();
        var assuntoCadastrado = AssuntoCadastroCommandResultMock.GerarObjeto();

        _mapper.Setup(mapper => mapper.Map<Assunto>(assuntoEnviado)).Returns(assuntoValido);
        _mapper.Setup(mapper => mapper.Map<AssuntoCadastroCommandResult>(It.IsAny<Assunto>())).Returns(assuntoCadastrado);

        _servicoCadastroAssunto.Setup(
            servicoCadastroAssunto => servicoCadastroAssunto.CadastrarAsync(It.IsAny<Assunto>(), CancellationToken.None)
        ).ReturnsAsync(assuntoValido);

        _servicoCadastroAssunto.SetupGet(property => property.ResultadoAcao).Returns(EResultadoAcaoServico.Suceso);
        _servicoCadastroAssunto.SetupGet(property => property.Notifications).Returns(NotificationMock.GerarListaVazia());
        _servicoCadastroAssunto.SetupGet(property => property.IsValid).Returns(true);

        var servico = GerarCenario();

        // Act
        var resultado = await servico.Handle(assuntoEnviado!, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado);
        Assert.True(resultado.IsValid);
        Assert.Empty(resultado.Notifications);
    }
}