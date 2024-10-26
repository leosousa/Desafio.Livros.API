namespace Livros.Dominio.Entidades;

/// <summary>
/// Armazena um assunto de livros
/// </summary>
public class Assunto : Entidade
{
    public Assunto(string descricao)
    {
        Descricao = descricao;
    }

    /// <summary>
    /// Descrição do assunto
    /// </summary>
    public string Descricao { get; private set; }
}