using FluentValidation;
using Livros.Dominio.Contratos;
using Livros.Dominio.Entidades;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Servicos.Assunto.Cadastrar;

public class ServicoCadastroAssunto : ServicoDominio, IServicoCadastroAssunto
{
    private readonly IRepositorioAssunto _repositorio;
    private readonly IValidator<Entidades.Assunto> _validator;

    public ServicoCadastroAssunto(
        IRepositorioAssunto repositorio,
        IValidator<Entidades.Assunto> validator)
    {
        _repositorio = repositorio;
        _validator = validator;
    }

    public async Task<Entidades.Assunto?> CadastrarAsync(Entidades.Assunto assunto, CancellationToken cancellationToken)
    {
        if (assunto is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotification(nameof(Entidades.Assunto), Mensagens.AssuntoNaoInformado);

            return await Task.FromResult<Entidades.Assunto?>(null);
        }

        var validationResult = await _validator.ValidateAsync(assunto);

        if (!validationResult.IsValid)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotifications(validationResult);

            return await Task.FromResult<Entidades.Assunto?>(null);
        }

        var assuntoCadastrado = await _repositorio.CadastrarAsync(assunto);

        if (assuntoCadastrado is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Erro);
            AddNotification(nameof(Entidades.Assunto), (Mensagens.OcorreuUmErroAoCadastrarAssunto));

            return await Task.FromResult<Entidades.Assunto?>(null);
        }

        AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Suceso);

        return await Task.FromResult(assuntoCadastrado);
    }
}