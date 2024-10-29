using FluentValidation;
using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Servicos.Autor.Cadastrar;

public class ServicoCadastroAutor : ServicoDominio, IServicoCadastroAutor
{
    private readonly IRepositorioAutor _repositorio;
    private readonly IValidator<Entidades.Autor> _validator;

    public ServicoCadastroAutor(
        IRepositorioAutor repositorio,
        IValidator<Entidades.Autor> validator)
    {
        _repositorio = repositorio;
        _validator = validator;
    }

    public async Task<Entidades.Autor?> CadastrarAsync(Entidades.Autor autor, CancellationToken cancellationToken)
    {
        if (autor is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotification(nameof(Entidades.Autor), Mensagens.AutorNaoInformado);

            return await Task.FromResult<Entidades.Autor?>(null);
        }

        var validationResult = await _validator.ValidateAsync(autor);

        if (!validationResult.IsValid)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotifications(validationResult);

            return await Task.FromResult<Entidades.Autor?>(null);
        }

        var autorCadastrado = await _repositorio.CadastrarAsync(autor);

        if (autorCadastrado is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Erro);
            AddNotification(nameof(Entidades.Autor), (Mensagens.OcorreuUmErroAoCadastrarAutor));

            return await Task.FromResult<Entidades.Autor?>(null);
        }

        AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Suceso);

        return await Task.FromResult(autorCadastrado);
    }
}