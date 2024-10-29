using Flunt.Notifications;

namespace Livros.Aplicacao.CasosUso.Autor.Editar;

public class AutorEdicaoCommandResult
{
    /// <summary>
    /// Identificador do autor
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descrição do autor
    /// </summary>
    public string Nome { get; set; }
}