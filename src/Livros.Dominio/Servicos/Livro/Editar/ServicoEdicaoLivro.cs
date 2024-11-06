using FluentValidation;
using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Servicos.Livro.Editar;

public class ServicoEdicaoLivro : ServicoDominio, IServicoEdicaoLivro
{
    private readonly IRepositorioLivro _repositorioLivro;
    private readonly IValidator<Entidades.Livro> _validator;

    public ServicoEdicaoLivro(
        IRepositorioLivro repositorioLivro, 
        IValidator<Entidades.Livro> validator)
    {
        _repositorioLivro = repositorioLivro;
        _validator = validator;
    }

    public async Task<Entidades.Livro?> EditarAsync(Entidades.Livro livro, CancellationToken cancellationToken)
    {
        if (livro is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotification(nameof(Entidades.Livro), Mensagens.LivroNaoInformado);

            return await Task.FromResult<Entidades.Livro?>(null);
        }

        var livroEncontrado = await _repositorioLivro.BuscarPorIdAsync(livro.Id);

        if (livroEncontrado is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.NaoEncontrado);
            AddNotification(nameof(Entidades.Livro), Mensagens.LivroNaoEncontrado);

            return await Task.FromResult<Entidades.Livro?>(null);
        }

        livroEncontrado.AlterarTitulo(livro.Titulo);
        livroEncontrado.AlterarEditora(livro.Editora);
        livroEncontrado.AlterarEdicao(livro.Edicao);
        livroEncontrado.AlterarAnoPublicacao(livro.AnoPublicacao);
        livroEncontrado.AlterarAutores(livro.Autores.ToList());
        livroEncontrado.AlterarAssuntos(livro.Assuntos.ToList());

        var validationResult = await _validator.ValidateAsync(livro);

        if (!validationResult.IsValid)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotifications(validationResult);

            return await Task.FromResult<Entidades.Livro?>(null);
        }

        var livroEditado = await _repositorioLivro.EditarAsync(livroEncontrado);

        if (livroEditado is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Erro);
            AddNotification(nameof(Entidades.Livro), (Mensagens.OcorreuUmErroAoEditarLivro));

            return await Task.FromResult<Entidades.Livro?>(null);
        }

        AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Suceso);

        return await Task.FromResult(livroEditado);
    }
}