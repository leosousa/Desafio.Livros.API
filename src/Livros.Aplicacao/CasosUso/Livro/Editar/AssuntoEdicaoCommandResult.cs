using Flunt.Notifications;

namespace Livros.Aplicacao.CasosUso.Livro.Editar;

public class LivroEdicaoCommandResult
{
    /// <summary>
    /// Identificador do livro
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descrição do livro
    /// </summary>
    public string Descricao { get; set; }
}