using Livros.Dominio.Entidades;

namespace Livros.Dominio.Recursos;

public static class Mensagens
{
    public const string AssuntoNaoInformado = "Assunto não informado";
    public const string AssuntoECampoObrigatorio = "Assunto é campo obrigatório";
    public static string AssuntoPodeTerAteXCaracteres = $"Assunto precisa ter no máximo {Assunto.ASSUNTO_DESCRICAO_MAXIMO_CARACTERES.ToString()} caracteres";
    public const string CodigoAssuntoInvalido = "Código do assunto inválido";
    public const string AssuntoNaoEncontrado = "Assunto não encontrado";
    public const string CodigoAssuntoNaoInformado = "Identificador do assunto não informado";
    public const string AssuntoNaoEditado = "Assunto não editado";
    public const string OcorreuUmErroAoEditarAssunto = "Ocorreu um erro ao editar o assunto";
    public const string OcorreuUmErroAoCadastrarAssunto = "Ocorreu um erro ao cadastrar o assunto";
}