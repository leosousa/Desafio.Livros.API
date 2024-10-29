namespace Livros.Dominio.Contratos.Servicos.Autor;

public interface IServicoBuscaAutorPorId : IServico
{
    Task<Entidades.Autor?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
}