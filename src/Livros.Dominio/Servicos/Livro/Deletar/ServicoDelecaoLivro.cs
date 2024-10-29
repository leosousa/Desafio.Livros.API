using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Servicos.Livro.Deletar;

public class ServicoDelecaoLivro : ServicoDominio, IServicoDelecaoLivro
{
    private readonly IRepositorioLivro _repositorioLivro;

    public ServicoDelecaoLivro(IRepositorioLivro repositorioLivro)
    {
        _repositorioLivro = repositorioLivro;
    }

    public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotification(nameof(id), Mensagens.CodigoLivroInvalido);

            return await Task.FromResult(false);
        }

        var livroParaDeletar = await _repositorioLivro.BuscarPorIdAsync(id);

        if (livroParaDeletar is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.NaoEncontrado);
            AddNotification(nameof(Entidades.Livro), Mensagens.LivroNaoEncontrado);

            return await Task.FromResult(false);
        }

        var livroRemovido = await _repositorioLivro.RemoverAsync(livroParaDeletar);

        if (!livroRemovido)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Erro);
            AddNotification(nameof(Entidades.Livro), Mensagens.LivroNaoDeletado);

            return await Task.FromResult(livroRemovido);
        }

        AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Suceso);

        return await Task.FromResult(livroRemovido);
    }
}