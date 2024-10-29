namespace Livros.Dominio.Contratos.Servicos.Autor;

public interface IServicoEdicaoAutor : IServico
{
    Task<Entidades.Autor?> EditarAsync(Entidades.Autor autor, CancellationToken cancellationToken);
}