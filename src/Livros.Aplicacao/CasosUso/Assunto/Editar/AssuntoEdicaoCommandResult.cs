using Flunt.Notifications;

namespace Livros.Aplicacao.CasosUso.Assunto.Editar;

public class AssuntoEdicaoCommandResult
{
    /// <summary>
    /// Identificador do assunto
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descrição do assunto
    /// </summary>
    public string Descricao { get; set; }
}