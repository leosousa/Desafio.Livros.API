using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Servicos.Livro.BuscarPorId;

public class ServicoBuscaLivroPorId : ServicoDominio, IServicoBuscaLivroPorId
{
    private readonly IRepositorioLivro _repositorioLivro;

    public ServicoBuscaLivroPorId(IRepositorioLivro repositorioLivro)
    {
        _repositorioLivro = repositorioLivro;
    }

    public async Task<Entidades.Livro?> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            AddNotification(nameof(id), Mensagens.CodigoLivroInvalido);

            return await Task.FromResult<Entidades.Livro?>(null);
        }

        var livroEncontrado = await _repositorioLivro.BuscarPorIdAsync(id);

        if (livroEncontrado is null)
        {
            return await Task.FromResult<Entidades.Livro?>(null);
        }

        return await Task.FromResult(livroEncontrado);
    }
}