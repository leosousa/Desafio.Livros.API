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

    public Assunto(string descricao)
    {
        Descricao = descricao;
    }

    public void AlterarDescricao(string descricao)
    {
        Descricao = Descricao;
    }

    #region Constantes
    public const int ASSUNTO_DESCRICAO_MAXIMO_CARACTERES = 20;
    #endregion
}