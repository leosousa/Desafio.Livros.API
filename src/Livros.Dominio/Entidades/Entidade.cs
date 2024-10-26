namespace Livros.Dominio.Entidades;

/// <summary>
/// Entidade base para todas as entidades
/// </summary>
public abstract class Entidade
{
    /// <summary>
    /// Código identificador do registro
    /// </summary>
    public int Id { get; protected set; }
}
