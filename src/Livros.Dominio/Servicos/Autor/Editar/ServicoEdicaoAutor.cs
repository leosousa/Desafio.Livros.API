using FluentValidation;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Servicos.Autor.Editar;

public class ServicoEdicaoAutor : ServicoDominio, IServicoEdicaoAutor
{
    private readonly IRepositorioAutor _repositorioAutor;
    private readonly IValidator<Entidades.Autor> _validator;

    public ServicoEdicaoAutor(
        IRepositorioAutor repositorioAutor, 
        IValidator<Entidades.Autor> validator)
    {
        _repositorioAutor = repositorioAutor;
        _validator = validator;
    }

    public async Task<Entidades.Autor?> EditarAsync(Entidades.Autor autor, CancellationToken cancellationToken)
    {
        if (autor is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotification(nameof(Entidades.Autor), Mensagens.AutorNaoInformado);

            return await Task.FromResult<Entidades.Autor?>(null);
        }

        var autorEncontrado = await _repositorioAutor.BuscarPorIdAsync(autor.Id);

        if (autorEncontrado is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.NaoEncontrado);
            AddNotification(nameof(Entidades.Autor), Mensagens.AutorNaoEncontrado);

            return await Task.FromResult<Entidades.Autor?>(null);
        }

        autorEncontrado.AlterarNome(autor.Nome);

        var validationResult = await _validator.ValidateAsync(autor);

        if (!validationResult.IsValid)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotifications(validationResult);

            return await Task.FromResult<Entidades.Autor?>(null);
        }

        var autorEditado = await _repositorioAutor.EditarAsync(autorEncontrado);

        if (autorEditado is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Erro);
            AddNotification(nameof(Entidades.Autor), (Mensagens.OcorreuUmErroAoEditarAutor));

            return await Task.FromResult<Entidades.Autor?>(null);
        }

        AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Suceso);

        return await Task.FromResult(autorEditado);
    }
}