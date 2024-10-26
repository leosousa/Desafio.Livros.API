namespace Livros.Dominio.Entidades;

/// <summary>
/// Armazena um assunto de livros
/// </summary>
public class Assunto : Entidade
{
    /// <summary>
    /// Descrição do assunto
    /// </summary>
    public string Descricao { get; private set; }
}