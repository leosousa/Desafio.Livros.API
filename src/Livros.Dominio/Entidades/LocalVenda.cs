namespace Livros.Dominio.Entidades;

public class LocalVenda : Entidade
{
    public string Descricao { get; private set; }

    #region Constantes
    public const int DESCRICAO_MAXIMO_CARACTERES = 255;
    #endregion
}