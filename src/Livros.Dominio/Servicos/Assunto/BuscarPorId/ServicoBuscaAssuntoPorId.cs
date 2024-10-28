using Flunt.Notifications;
using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Servicos.Assunto.BuscarPorId;

public class ServicoBuscaAssuntoPorId : ServicoDominio, IServicoBuscaAssuntoPorId
{
    private readonly IRepositorioAssunto _repositorioAssunto;

    public ServicoBuscaAssuntoPorId(IRepositorioAssunto repositorioAssunto)
    {
        _repositorioAssunto = repositorioAssunto;
    }

    public async Task<Entidades.Assunto?> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            AddNotification(nameof(id), Mensagens.CodigoAssuntoInvalido);

            return await Task.FromResult<Entidades.Assunto?>(null);
        }

        var assuntoEncontrado = await _repositorioAssunto.BuscarPorIdAsync(id);

        if (assuntoEncontrado is null)
        {
            return await Task.FromResult<Entidades.Assunto?>(null);
        }

        return await Task.FromResult(assuntoEncontrado);
    }
}