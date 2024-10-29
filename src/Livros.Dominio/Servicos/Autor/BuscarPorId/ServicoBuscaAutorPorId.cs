using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Servicos.Autor.BuscarPorId;

public class ServicoBuscaAutorPorId : ServicoDominio, IServicoBuscaAutorPorId
{
    private readonly IRepositorioAutor _repositorioAutor;

    public ServicoBuscaAutorPorId(IRepositorioAutor repositorioAutor)
    {
        _repositorioAutor = repositorioAutor;
    }

    public async Task<Entidades.Autor?> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            AddNotification(nameof(id), Mensagens.CodigoAutorInvalido);

            return await Task.FromResult<Entidades.Autor?>(null);
        }

        var autorEncontrado = await _repositorioAutor.BuscarPorIdAsync(id);

        if (autorEncontrado is null)
        {
            return await Task.FromResult<Entidades.Autor?>(null);
        }

        return await Task.FromResult(autorEncontrado);
    }
}