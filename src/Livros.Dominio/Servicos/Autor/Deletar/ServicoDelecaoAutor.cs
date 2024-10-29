using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Servicos.Autor.Deletar;

public class ServicoDelecaoAutor : ServicoDominio, IServicoDelecaoAutor
{
    private readonly IRepositorioAutor _repositorioAutor;

    public ServicoDelecaoAutor(IRepositorioAutor repositorioAutor)
    {
        _repositorioAutor = repositorioAutor;
    }

    public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotification(nameof(id), Mensagens.CodigoAutorInvalido);

            return await Task.FromResult(false);
        }

        var autorParaDeletar = await _repositorioAutor.BuscarPorIdAsync(id);

        if (autorParaDeletar is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.NaoEncontrado);
            AddNotification(nameof(Entidades.Autor), Mensagens.AutorNaoEncontrado);

            return await Task.FromResult(false);
        }

        var autorRemovido = await _repositorioAutor.RemoverAsync(autorParaDeletar);

        if (!autorRemovido)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Erro);
            AddNotification(nameof(Entidades.Autor), Mensagens.AutorNaoDeletado);

            return await Task.FromResult(autorRemovido);
        }

        AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Suceso);

        return await Task.FromResult(autorRemovido);
    }
}