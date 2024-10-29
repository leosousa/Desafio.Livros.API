using FluentValidation;
using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Servicos.Livro.Cadastrar;

public class ServicoCadastroLivro : ServicoDominio, IServicoCadastroLivro
{
    private readonly IRepositorioLivro _repositorio;
    private readonly IValidator<Entidades.Livro> _validator;

    public ServicoCadastroLivro(
        IRepositorioLivro repositorio,
        IValidator<Entidades.Livro> validator)
    {
        _repositorio = repositorio;
        _validator = validator;
    }

    public async Task<Entidades.Livro?> CadastrarAsync(Entidades.Livro livro, CancellationToken cancellationToken)
    {
        if (livro is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotification(nameof(Entidades.Livro), Mensagens.LivroNaoInformado);

            return await Task.FromResult<Entidades.Livro?>(null);
        }

        var validationResult = await _validator.ValidateAsync(livro);

        if (!validationResult.IsValid)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotifications(validationResult);

            return await Task.FromResult<Entidades.Livro?>(null);
        }

        var livroCadastrado = await _repositorio.CadastrarAsync(livro);

        if (livroCadastrado is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Erro);
            AddNotification(nameof(Entidades.Livro), (Mensagens.OcorreuUmErroAoCadastrarLivro));

            return await Task.FromResult<Entidades.Livro?>(null);
        }

        AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Suceso);

        return await Task.FromResult(livroCadastrado);
    }
}