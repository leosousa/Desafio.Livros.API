using FluentValidation;
using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Servicos.Assunto.Editar;

public class ServicoEdicaoAssunto : ServicoDominio, IServicoEdicaoAssunto
{
    private readonly IRepositorioAssunto _repositorioAssunto;
    private readonly IValidator<Entidades.Assunto> _validator;

    public ServicoEdicaoAssunto(
        IRepositorioAssunto repositorioAssunto, 
        IValidator<Entidades.Assunto> validator)
    {
        _repositorioAssunto = repositorioAssunto;
        _validator = validator;
    }

    public async Task<Entidades.Assunto?> EditarAsync(Entidades.Assunto assunto, CancellationToken cancellationToken)
    {
        if (assunto is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotification(nameof(Entidades.Assunto), Mensagens.AssuntoNaoInformado);

            return await Task.FromResult<Entidades.Assunto?>(null);
        }

        var assuntoEncontrado = await _repositorioAssunto.BuscarPorIdAsync(assunto.Id);

        if (assuntoEncontrado is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.NaoEncontrado);
            AddNotification(nameof(Entidades.Assunto), Mensagens.AssuntoNaoEncontrado);

            return await Task.FromResult<Entidades.Assunto?>(null);
        }

        assuntoEncontrado.AlterarDescricao(assunto.Descricao);

        var validationResult = await _validator.ValidateAsync(assunto);

        if (!validationResult.IsValid)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotifications(validationResult);

            return await Task.FromResult<Entidades.Assunto?>(null);
        }

        var assuntoEditado = await _repositorioAssunto.EditarAsync(assuntoEncontrado);

        if (assuntoEditado is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Erro);
            AddNotifications(validationResult);

            return await Task.FromResult<Entidades.Assunto?>(null);
        }

        AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Suceso);

        return await Task.FromResult(assuntoEditado);
    }
}