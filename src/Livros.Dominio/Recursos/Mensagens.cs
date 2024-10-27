using Livros.Dominio.Entidades;

namespace Livros.Dominio.Recursos;

public static class Mensagens
{
    public const string AssuntoNaoInformado = "Assunto não informado";
    public const string AssuntoECampoObrigatorio = "Assunto é campo obrigatório";
    public static string AssuntoPodeTerAteXCaracteres = $"Assunto precisa ter no máximo {Assunto.ASSUNTO_DESCRICAO_MAXIMO_CARACTERES.ToString()} caracteres";
}