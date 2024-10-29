using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Servicos.Assunto.Deletar;

public class ServicoDelecaoAssunto : ServicoDominio, IServicoDelecaoAssunto
{
    private readonly IRepositorioAssunto _repositorioAssunto;

    public ServicoDelecaoAssunto(IRepositorioAssunto repositorioAssunto)
    {
        _repositorioAssunto = repositorioAssunto;
    }

    public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.ParametrosInvalidos);
            AddNotification(nameof(id), Mensagens.CodigoAssuntoInvalido);

            return await Task.FromResult(false);
        }

        var assuntoParaDeletar = await _repositorioAssunto.BuscarPorIdAsync(id);

        if (assuntoParaDeletar is null)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.NaoEncontrado);
            AddNotification(nameof(Entidades.Assunto), Mensagens.AssuntoNaoEncontrado);

            return await Task.FromResult(false);
        }

        var assuntoRemovido = await _repositorioAssunto.RemoverAsync(assuntoParaDeletar);

        if (!assuntoRemovido)
        {
            AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Erro);
            AddNotification(nameof(Entidades.Assunto), Mensagens.AssuntoNaoRDeletado);

            return await Task.FromResult(assuntoRemovido);
        }

        AddResultadoAcao(Enumeracoes.EResultadoAcaoServico.Suceso);

        return await Task.FromResult(assuntoRemovido);
    }
}