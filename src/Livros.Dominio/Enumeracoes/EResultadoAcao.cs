namespace Livros.Dominio.Enumeracoes;

/// <summary>
/// Armazena informação de resultado da ação de um serviço
/// </summary>
/// <remarks>Propaga informação para classe de serviço da aplicação e controller 
/// a fim de devolver corretamete o status para quem chamou. Significados:
/// ParametrosInvalidos: sinaliza BadRequest (400)
/// NaoEncontrado: sinaliza NotFound (404)
/// Sucesso: sinaliza Success (200)
/// Erro: sinaliza InternalServerError (500)
/// </remarks>
public enum EResultadoAcaoServico
{
    // Sinaliza BadRequest (400) 
    ParametrosInvalidos,

    // Sinaliza NotFound (404)
    NaoEncontrado,

    // Sinaliza Success (200)
    Suceso,

    // Sinaliza InternalServerError (500)
    Erro
}